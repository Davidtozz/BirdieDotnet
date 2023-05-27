using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Services
{
    public sealed class SignalRService
    {
        [Required]
        private HubConnection Connection;

        public SignalRService(string HubUrl)
        {
            Connection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();
        }

        public async Task StartConnection()
        {
            // Registering handlers
            Connection.On<string, string>("ReceiveMessage", OnReceiveMessage);
            Connection.On<string>("UserDisconnected", OnUserDisconnected);

            try
            {
                await Connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        #region EventDispatchers

        public async Task SendUserInput() // TEST METHOD
        {
            string input;
            do
            {
                input = Console.ReadLine() ?? "_";
                await Connection.InvokeAsync("SendMessage", input);
            }
            while (input != "exit");

            // Interrupt SignalR if input == "exit"
            await Connection.StopAsync();
            await Connection.DisposeAsync();
        }

        #endregion

        #region EventHandlers

        private void OnReceiveMessage(string connectionId, string message)
        {
            Console.WriteLine($"Received message {message} from {connectionId}");
        }

        private void OnUserDisconnected(string connectionId)
        {
            Console.WriteLine($"User disconnected: {connectionId}");
        }

        #endregion
    }
}

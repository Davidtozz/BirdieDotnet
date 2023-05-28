using BirdieDotnetCLI.Models;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirdieDotnetCLI.Services
{
    public sealed class ChatService : IDisposable
    {
        
        private HubConnection Connection;
        public User currentUser { get; set;}
        private bool disposedValue;

        public ChatService(string HubUrl)
        {
            Connection = new HubConnectionBuilder()
            .WithUrl(HubUrl)
            .WithAutomaticReconnect()
            .Build();
        }

        public async Task<string> StartConnection()
        {
            RegisterHandlers();

            try
            {
                await Connection.StartAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return Connection.ConnectionId;
        }

        public async Task StopConnection()
        {
            await Connection.StopAsync();
            await Connection.DisposeAsync();
        }

        private void RegisterHandlers()
        {
            // Registering handlers
            Connection.On<string, string, User>("ReceiveMessage", OnReceiveMessage);
            Connection.On<User>("UserDisconnected", OnUserDisconnected);
        }

        #region EventDispatchers

        public async Task SendMessage(string text, User sender)
        {
            await Connection.InvokeAsync("SendMessage", text, sender);
            Console.WriteLine(); // Add a line break after sending a message
        }

        #endregion

        #region EventHandlers

        private void OnReceiveMessage(string connectionId, string message, User fromUser)
        {
            if (connectionId != currentUser.ConnectionId)
            {
                Console.WriteLine($"\n\u001b[32m{fromUser.Name}:\u001b[0m {message}");
            }
            /*else
            {
                Console.WriteLine($"\u001b[32m (You):\u001b[0m {message}");
            }*/
        }

        private void OnUserDisconnected(User disconnectingUser)
        {
            Console.WriteLine($"User \u001b[32m{disconnectingUser.Name}\u001b[0m disconnected");
        }

        #endregion


        private void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects)
                }

                // TODO: free unmanaged resources (unmanaged objects) and override finalizer
                // TODO: set large fields to null
                disposedValue = true;
            }
        }

        // // TODO: override finalizer only if 'Dispose(bool disposing)' has code to free unmanaged resources
        // ~SignalRService()
        // {
        //     // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        //     Dispose(disposing: false);
        // }

        public void Dispose()
        {
            // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }

    }
}

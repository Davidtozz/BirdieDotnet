using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BirdieDotnetAPI.Hubs
{
    

    public class ChatHub : Hub
    {
        //TODO Add support for 1-to-1 conversations

        //! TEST ATTRIBUTE
        public static uint ConnectedClients = 0;
        private Dictionary<string,dynamic> UserMapping = new();
        #region EventDispatchers        
       
        public async Task SendMessage(string message)
        {
            string connectionId = Context.ConnectionId;

            //? Trigger onReceiveMessage client-side
            await Clients.All.SendAsync("ReceiveMessage", connectionId, message);
        }

        #endregion 

        #region EventHandlers

        public override async Task OnConnectedAsync()
        {
            //TODO extract JWT from headers to identify user
            string connectionId = Context.ConnectionId;
            ConnectedClients++;
            Console.WriteLine($"Client ({connectionId}) connected. Current clients: {ConnectedClients}");

            await base.OnConnectedAsync();
        }

        #nullable disable 
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            string connectionId = Context.ConnectionId;
            ConnectedClients--;
            Console.WriteLine($"Client ({connectionId}) disconnected. Clients: {ConnectedClients}");

            await Clients.All.SendAsync("UserDisconnected", connectionId);

            await base.OnDisconnectedAsync(exception);
        }

        #endregion

    }
}

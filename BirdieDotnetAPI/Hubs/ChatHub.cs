using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BirdieDotnetAPI.Hubs
{
    public class ChatHub : Hub
    {

        //TODO Cache ConnectionId and username for 1-to-1 conversations
        //TODO Create groups for each individual user 

        // TEST ATTRIBUTE
        public static uint ConnectedClients = 0;

        #region EventDispatchers

        // TEST METHOD
        public async Task SendMessage(string message, User fromUser)
        {
            string connectionId = Context.ConnectionId;

            await Clients.All.SendAsync("ReceiveMessage", connectionId , message, fromUser);
        }

        #endregion 

        #region EventHandlers

        public override async Task OnConnectedAsync()
        {

            string connectionId = Context.ConnectionId;
            ConnectedClients++;
            Console.WriteLine($"Client ({connectionId}) connected. Current clients: {ConnectedClients}");

            await base.OnConnectedAsync();
        }

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

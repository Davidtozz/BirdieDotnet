using BirdieDotnetAPI.Models;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace BirdieDotnetAPI.Hubs
{
    public class ChatHub : Hub
    {
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
            // Perform any desired tracking or logic when a client connects
            // For example, you can store the connection ID or perform other actions

            string connectionId = Context.ConnectionId;
            ConnectedClients++;
            Console.WriteLine($"Client ({connectionId}) connected. Current clients: {ConnectedClients}");
            // You can store or process the connection ID as needed

            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            // Perform any desired cleanup or logic when a client disconnects
            // For example, you can remove the connection ID from a list or update user states

            string connectionId = Context.ConnectionId;
            ConnectedClients--;
            Console.WriteLine($"Client ({connectionId}) disconnected. Clients: {ConnectedClients}");

            await Clients.All.SendAsync("UserDisconnected", connectionId);
            // You can use the connection ID to identify and remove the disconnected client

            await base.OnDisconnectedAsync(exception);
        }

        #endregion

    }
}

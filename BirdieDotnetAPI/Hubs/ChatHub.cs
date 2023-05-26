using Microsoft.AspNetCore.SignalR;

namespace BirdieDotnetAPI.Hubs
{
    public class ChatHub : Hub
    {
        //Test
        public static int TotalViews { get; set; }

        public async Task NewWindowLoaded() 
        {
            TotalViews++;
            Console.WriteLine($"A Client Updated TotalViews: {TotalViews}");
            await Clients.All.SendAsync("updateTotalViews", TotalViews);
        }
    }
}

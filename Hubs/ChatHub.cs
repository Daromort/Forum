using Microsoft.AspNetCore.SignalR;

namespace Forum_Management_System.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
            //await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

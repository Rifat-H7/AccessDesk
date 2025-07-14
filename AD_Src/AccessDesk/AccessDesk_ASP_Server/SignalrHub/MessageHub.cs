using Microsoft.AspNetCore.SignalR;

namespace AccessDesk_ASP_Server.SignalrHub
{
    public class MessageHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}

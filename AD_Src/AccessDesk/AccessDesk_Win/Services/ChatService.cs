using AccessDesk_Win.Models;
using AccessDesk_Win.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDesk_Win.Services
{
    internal class ChatService : IChatService
    {
        private readonly IHubConnectionService _hub;
        public ObservableCollection<ChatMessage> Messages { get; } = new();
        public bool IsConnected => _hub.IsConnected;

        public event Action<bool> ConnectionChanged;

        public ChatService(IHubConnectionService hub)
        {
            _hub = hub;
            _hub.Connection.On<string, string>("ReceiveMessage", (user, message) =>
            {
                Messages.Add(new ChatMessage { User = user, Message = message });
            });

            _hub.ConnectionChanged += connected => ConnectionChanged?.Invoke(connected);
        }

        public async Task ConnectAsync()
        {
            await _hub.ConnectAsync();
        }

        public async Task SendMessageAsync(string user, string message)
        {
            if (_hub.IsConnected)
                await _hub.Connection.InvokeAsync("SendMessage", user, message);
        }
    }
}

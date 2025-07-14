using AccessDesk_Win.Services.Interfaces;
using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDesk_Win.Services
{
    internal class HubConnectionService : IHubConnectionService
    {
        public HubConnection Connection { get; }
        public bool IsConnected => Connection.State == HubConnectionState.Connected;
        public event Action<bool> ConnectionChanged;

        public HubConnectionService()
        {
            Connection = new HubConnectionBuilder()
                .WithUrl("http://localhost:5000/chatHub") // Unified hub
                .WithAutomaticReconnect()
                .Build();

            Connection.Reconnecting += error => { ConnectionChanged?.Invoke(false); return Task.CompletedTask; };
            Connection.Reconnected += id => { ConnectionChanged?.Invoke(true); return Task.CompletedTask; };
            Connection.Closed += error => { ConnectionChanged?.Invoke(false); return Task.CompletedTask; };
        }

        public async Task ConnectAsync()
        {
            if (Connection.State == HubConnectionState.Disconnected)
            {
                await Connection.StartAsync();
                ConnectionChanged?.Invoke(true);
            }
        }
    }
}

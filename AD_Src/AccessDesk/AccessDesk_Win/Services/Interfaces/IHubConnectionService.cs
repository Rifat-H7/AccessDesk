using Microsoft.AspNetCore.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDesk_Win.Services.Interfaces
{
    internal interface IHubConnectionService
    {
        HubConnection Connection { get; }
        bool IsConnected { get; }
        event Action<bool> ConnectionChanged;

        Task ConnectAsync();
    }
}

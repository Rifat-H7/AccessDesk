using AccessDesk_Win.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessDesk_Win.Services.Interfaces
{
    public interface IChatService
    {
        ObservableCollection<ChatMessage> Messages { get; }
        bool IsConnected { get; }
        event Action<bool> ConnectionChanged;
        Task ConnectAsync();
        Task SendMessageAsync(string user, string message);
    }
}

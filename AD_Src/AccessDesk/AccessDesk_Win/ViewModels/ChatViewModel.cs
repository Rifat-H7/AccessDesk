using AccessDesk_Win.Models;
using AccessDesk_Win.Services;
using AccessDesk_Win.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AccessDesk_Win.ViewModels
{
    public partial class ChatViewModel : ViewModel
    {
        private readonly IChatService _chatService;
        public ChatViewModel(IChatService chatService)
        {
            _chatService = chatService;
            _chatService.Messages.CollectionChanged += Messages_CollectionChanged;
            Task.Run(async () => await _chatService.ConnectAsync());
        }

        [ObservableProperty]
        private string? message;

        [RelayCommand]
        private void SendMessage()
        {
            _chatService.SendMessageAsync("zawad", Message);
        }

        private void Messages_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add && e.NewItems != null)
            {
                foreach (var item in e.NewItems)
                {
                    if (item is ChatMessage chatMessage)
                    {
                        // Show the message box on the UI thread
                        Application.Current.Dispatcher.Invoke(() =>
                        {
                            MessageBox.Show($"New message from {chatMessage.User}: {chatMessage.Message}", "New Chat Message");
                        });
                    }
                }
            }
        }
    }
}

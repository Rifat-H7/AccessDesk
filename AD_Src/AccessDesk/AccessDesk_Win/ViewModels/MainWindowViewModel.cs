using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;
using Wpf.Ui.Controls;

namespace AccessDesk_Win.ViewModels
{
    public partial class MainWindowViewModel : ViewModel
    {
        private bool _isInitialized = false;

        [ObservableProperty]
        private string _applicationTitle = string.Empty;

        [ObservableProperty]
        private ObservableCollection<object> _navigationItems = [];

        [ObservableProperty]
        private ObservableCollection<object> _navigationFooter = [];

        [ObservableProperty]
        private ObservableCollection<MenuItem> _trayMenuItems = [];

        [System.Diagnostics.CodeAnalysis.SuppressMessage(
            "Style",
            "IDE0060:Remove unused parameter",
            Justification = "Demo"
        )]
        public MainWindowViewModel(INavigationService navigationService)
        {
            if (!_isInitialized)
            {
                InitializeViewModel();
            }
        }

        private void InitializeViewModel()
        {
            ApplicationTitle = "AccessDesk";

            NavigationItems =
            [
                new NavigationViewItem()
                {
                    Content = "Home",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Home24 },
                    TargetPageType = typeof(Views.Pages.HomePage),
                },
                new NavigationViewItem()
                {
                    Content = "Chat",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Chat24 },
                    TargetPageType = typeof(Views.Pages.ChatPage),
                },
            ];

            NavigationFooter =
            [
                new NavigationViewItem()
                {
                    Content = "Settings",
                    Icon = new SymbolIcon { Symbol = SymbolRegular.Settings24 },
                    TargetPageType = typeof(Views.Pages.SettingsPage),
                },
            ];

            TrayMenuItems = [new() { Header = "Home", Tag = "tray_home" }];

            _isInitialized = true;
        }
    }
}

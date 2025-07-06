using AccessDesk_Win.Views;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Wpf.Ui;

namespace AccessDesk_Win.Services
{
    public class ApplicationHostService(IServiceProvider serviceProvider) : IHostedService
    {
        private INavigationWindow? _navigationWindow;

        /// <summary>
        /// Triggered when the application host is ready to start the service.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the start process has been aborted.</param>
        public async Task StartAsync(CancellationToken cancellationToken)
        {
            await HandleActivationAsync();
        }

        /// <summary>
        /// Triggered when the application host is performing a graceful shutdown.
        /// </summary>
        /// <param name="cancellationToken">Indicates that the shutdown process should no longer be graceful.</param>
        public async Task StopAsync(CancellationToken cancellationToken)
        {
            await Task.CompletedTask;
        }

        /// <summary>
        /// Creates main window during activation.
        /// </summary>
        private async Task HandleActivationAsync()
        {
            await Task.CompletedTask;

            if (!App.Current.Windows.OfType<MainWindow>().Any())
            {
                _navigationWindow = (serviceProvider.GetService(typeof(INavigationWindow)) as INavigationWindow)!;
                _navigationWindow!.ShowWindow();

                _ = _navigationWindow.Navigate(typeof(Views.Pages.HomePage));
            }

            await Task.CompletedTask;
        }
    }
}

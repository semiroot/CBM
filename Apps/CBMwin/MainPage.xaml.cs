using System;
using System.Diagnostics;

using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

using CBM.Core.Com;
using CBM.Core.Com.Notifications;
using CBM.Core.Udp;

namespace CBM
{
    /// <summary>
    /// The main page controller.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        /// <summary>
        /// The udp handler instance.
        /// </summary>
        private readonly UdpHandler udpHandler = new UdpHandler();

        /// <summary>
        /// Initialization and subscription.
        /// </summary>
        public MainPage()
        {
            InitializeComponent();

            NotificationHub.NotificationPublished += HandleNotification;

            udpHandler.StartListening();
        }

        /// <summary>
        /// Handles notifications published on the hub.
        /// </summary>
        /// <param name="sender">Sender.</param>
        /// <param name="n">Notification.</param>
        private async void HandleNotification(object sender, Notification n)
        {
            switch (n) {
                case MessageReceivedNotification rn:
                    // Update ui.
                    await Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () => {
                        MessageBlock.Text = $"{rn.Message}\n----------\n{MessageBlock.Text}";
                    });
                    break;
                case LogNotification ln:
                    Debug.WriteLine(ln.Message);
                    break;
            }
        }

        /// <summary>
        /// Handles an enter key press to initiate broadcasting of the set message.
        /// </summary>
        private async void HandleReturn(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key != Windows.System.VirtualKey.Enter) { return; }

            var message = MessageText.Text.Trim();
            MessageText.Text = "";

            if (string.IsNullOrWhiteSpace(message)) { return; }

            await udpHandler.BroadcastMessage(message);
        }
    }
}

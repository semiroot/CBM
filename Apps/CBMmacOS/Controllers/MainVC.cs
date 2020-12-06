using System;
using System.Diagnostics;

using Foundation;
using AppKit;

using CBM.Core.Com;
using CBM.Core.Com.Notifications;
using CBM.Core.Udp;

namespace CBM.Controllers
{
    /// <summary>
    /// Simple view controller for the main/only page.
    /// </summary>
    public partial class MainVC : NSViewController
    {
        /// <summary>
        /// The udp handler instance.
        /// </summary>
        private readonly UdpHandler udpHandler = new UdpHandler(); 

        /// <inheritdoc/>
        public MainVC(IntPtr handle) : base(handle) { }
        
        /// <summary>
        /// Event subscription.
        /// </summary>
        /// <inheritdoc/>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Subscribe to published messages.
            NotificationHub.NotificationPublished += (_, n) => {
                switch (n) {
                    case MessageReceivedNotification ln:
                        // Update the ui view when a new message is received.
                        NSRunLoop.Main.BeginInvokeOnMainThread(() => {
                            var lValue = $"{ln.Message}\n----------\n{MessageView.Value}";
                            MessageView.Value = lValue;
                        });
                        break;
                    case LogNotification sn:
                        // Send log to debug console.
                        Debug.WriteLine(sn.Message);
                        break;
                }
            };
            
            MessageText.EditingEnded += HandleMessageReady;
            ButtonMessage.Activated += HandleMessageReady;
            
            // We are ready lets get some messages.
            udpHandler.StartListening();
        }

        /// <summary>
        /// Sends the message entered in the interface.
        /// </summary>
        private async void HandleMessageReady(object sender, EventArgs e)
        {
            var message = MessageText.StringValue;
            MessageText.StringValue = "";
            
            await udpHandler.BroadcastMessage(message);
        }
    }
}

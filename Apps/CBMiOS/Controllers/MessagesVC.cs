using System;
using System.Threading.Tasks;

using CBM.Core.Com;
using CBM.Core.Com.Notifications;
using CBM.Core.Udp;

using Foundation;

using UIKit;

namespace CBM.Controllers
{
    /// <summary>
    /// Basic view controller for the messages screen,
    /// handling udp setup too.
    /// </summary>
    public partial class MessagesVC : UIViewController
    {
        /// <summary>
        /// The udp handler instance.
        /// </summary>
        private readonly UdpHandler udpHandler = new UdpHandler(); 
        
        /// <inheritdoc/>
        public MessagesVC(IntPtr handle) : base(handle) { }

        /// <summary>
        /// Setup subscriptions.
        /// </summary>
        /// <inheritdoc/>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Register to received broadcast messages.
            NotificationHub.NotificationPublished += (_, n) => {
                if (!(n is MessageReceivedNotification)) { return; }
                
                // Update ui.
                NSRunLoop.Main.BeginInvokeOnMainThread(() => {
                    ContentView.Text = $"{n.Message}\n----------\n{ContentView.Text ?? ""}";
                });
            };

            // Lets listen to broadcast messages.
            udpHandler.StartListening();
            
            // We need to send a message for the systems permission dialog to appear.
            _ = Task.Run(() => udpHandler.BroadcastMessage("Permission Check"));
        }
    }
}
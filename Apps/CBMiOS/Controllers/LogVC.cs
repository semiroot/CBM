using System;

using CBM.Core.Com;
using CBM.Core.Com.Notifications;

using Foundation;

using UIKit;

namespace CBM.Controllers
{
    /// <summary>
    /// Basic view controller for the log screen.
    /// </summary>
    public partial class LogVC : UIViewController
    {
        /// <inheritdoc/>
        public LogVC(IntPtr handle) : base(handle) { }

        /// <summary>
        /// Setup subscriptions.
        /// </summary>
        /// <inheritdoc/>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Register to log messages.
            NotificationHub.NotificationPublished += (_, n) => {
                if (!(n is LogNotification)) { return; }
                
                // Update ui.
                NSRunLoop.Main.BeginInvokeOnMainThread(() => {
                    ContentView.Text = $"{n.Message}\n{ContentView.Text ?? ""}";
                });
            };
        }
    }
}


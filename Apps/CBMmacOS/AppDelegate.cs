using AppKit;

using Foundation;

namespace CBM
{
    [Register("AppDelegate")]
    public class AppDelegate : NSApplicationDelegate
    {
        /// <summary>
        /// Makes sure the app is terminated when the window is closed.
        /// </summary>
        [Export("applicationShouldTerminateAfterLastWindowClosed:")]
        public override bool ApplicationShouldTerminateAfterLastWindowClosed(NSApplication sender) => true;
    }
}

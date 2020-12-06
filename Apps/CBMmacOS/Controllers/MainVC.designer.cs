// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace CBM.Controllers
{
	[Register ("MainVC")]
	partial class MainVC
	{
		[Outlet]
		AppKit.NSButton ButtonMessage { get; set; }

		[Outlet]
		AppKit.NSTextField MessageText { get; set; }

		[Outlet]
		AppKit.NSTextView MessageView { get; set; }

		void ReleaseDesignerOutlets ()
		{
			if (ButtonMessage != null) {
				ButtonMessage.Dispose ();
				ButtonMessage = null;
			}

			if (MessageText != null) {
				MessageText.Dispose ();
				MessageText = null;
			}

			if (MessageView != null) {
				MessageView.Dispose ();
				MessageView = null;
			}

		}
	}
}

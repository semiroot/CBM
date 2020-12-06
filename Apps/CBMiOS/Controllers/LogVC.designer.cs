// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//

using Foundation;

namespace CBM.Controllers
{
	[Register ("LogVC")]
	partial class LogVC
	{
		[Outlet]
		UIKit.UITextView ContentView { get; set; }
		
		void ReleaseDesignerOutlets ()
		{
			if (ContentView != null) {
				ContentView.Dispose ();
				ContentView = null;
			}
		}
	}
}

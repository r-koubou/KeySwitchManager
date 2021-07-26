// WARNING
//
// This file has been generated automatically by Rider IDE
//   to store outlets and actions made in Xcode.
// If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace KeySwitchManager.Xamarin.Mac
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextView LogTextView { get; set; }

		[Outlet]
		AppKit.NSTextField NewFileTextField { get; set; }

		[Outlet]
		AppKit.NSButton OpenNewFileButton { get; set; }

		[Action ("OnCreateDefinitionFileChooserButtonClicked:")]
		partial void OnCreateDefinitionFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenNewFileButtonClicked:")]
		partial void OnOpenNewFileButtonClicked (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (LogTextView != null) {
				LogTextView.Dispose ();
				LogTextView = null;
			}

			if (NewFileTextField != null) {
				NewFileTextField.Dispose ();
				NewFileTextField = null;
			}

			if (OpenNewFileButton != null) {
				OpenNewFileButton.Dispose ();
				OpenNewFileButton = null;
			}

		}
	}
}

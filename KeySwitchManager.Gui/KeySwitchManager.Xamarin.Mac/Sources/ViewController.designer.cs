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
		AppKit.NSTextField ImportDatabaseFileText { get; set; }

		[Outlet]
		AppKit.NSTextField ImportFileText { get; set; }

		[Outlet]
		AppKit.NSTextView LogTextView { get; set; }

		[Outlet]
		AppKit.NSTextField NewFileText { get; set; }

		[Outlet]
		AppKit.NSButton OpenNewFileButton { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator ProgressBar { get; set; }

		[Action ("OnCreateDefinitionFileChooserButtonClicked:")]
		partial void OnCreateDefinitionFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnDoImportButtonClicked:")]
		partial void OnDoImportButtonClicked (Foundation.NSObject sender);

		[Action ("OnLogClearButtonClicked:")]
		partial void OnLogClearButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenDatabaseFileChooserButtonClicked:")]
		partial void OnOpenDatabaseFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenFileChooserButtonClicked:")]
		partial void OnOpenFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenNewFileButtonClicked:")]
		partial void OnOpenNewFileButtonClicked (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (ImportDatabaseFileText != null) {
				ImportDatabaseFileText.Dispose ();
				ImportDatabaseFileText = null;
			}

			if (ImportFileText != null) {
				ImportFileText.Dispose ();
				ImportFileText = null;
			}

			if (LogTextView != null) {
				LogTextView.Dispose ();
				LogTextView = null;
			}

			if (NewFileText != null) {
				NewFileText.Dispose ();
				NewFileText = null;
			}

			if (OpenNewFileButton != null) {
				OpenNewFileButton.Dispose ();
				OpenNewFileButton = null;
			}

			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}

		}
	}
}

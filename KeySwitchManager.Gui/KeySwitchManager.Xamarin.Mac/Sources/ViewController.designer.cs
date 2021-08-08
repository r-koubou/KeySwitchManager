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
		AppKit.NSTextField ExportDirectoryText { get; set; }

		[Outlet]
		AppKit.NSPopUpButton ExportFormatCombobox { get; set; }

		[Outlet]
		AppKit.NSTextField FindDatabaseFileText { get; set; }

		[Outlet]
		AppKit.NSTextField FindDeveloperText { get; set; }

		[Outlet]
		AppKit.NSTextField FindInstrumentText { get; set; }

		[Outlet]
		AppKit.NSTextField FindProductText { get; set; }

		[Outlet]
		AppKit.NSTextField ImportDatabaseFileText { get; set; }

		[Outlet]
		AppKit.NSTextField ImportFileText { get; set; }

		[Outlet]
		AppKit.NSTextView LogTextView { get; set; }

		[Outlet]
		AppKit.NSTextField NewFileText { get; set; }

		[Outlet]
		AppKit.NSProgressIndicator ProgressBar { get; set; }

		[Outlet]
		AppKit.NSTabView TabView { get; set; }

		[Action ("OnCreateDefinitionFileChooserButtonClicked:")]
		partial void OnCreateDefinitionFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnDoImportButtonClicked:")]
		partial void OnDoImportButtonClicked (Foundation.NSObject sender);

		[Action ("OnExportButtonClicked:")]
		partial void OnExportButtonClicked (Foundation.NSObject sender);

		[Action ("OnFindButtonClicked:")]
		partial void OnFindButtonClicked (Foundation.NSObject sender);

		[Action ("OnLogClearButtonClicked:")]
		partial void OnLogClearButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenDatabaseFileChooserButtonClicked:")]
		partial void OnOpenDatabaseFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenFileChooserButtonClicked:")]
		partial void OnOpenFileChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OnOpenNewFileButtonClicked:")]
		partial void OnOpenNewFileButtonClicked (Foundation.NSObject sender);

		[Action ("OnSaveExportDirectoryChooserButtonClicked:")]
		partial void OnSaveExportDirectoryChooserButtonClicked (Foundation.NSObject sender);

		[Action ("OpenFindDatabaseFileChooserButtonClicked:")]
		partial void OpenFindDatabaseFileChooserButtonClicked (Foundation.NSObject sender);

		void ReleaseDesignerOutlets ()
		{
			if (TabView != null) {
				TabView.Dispose ();
				TabView = null;
			}

			if (ExportDirectoryText != null) {
				ExportDirectoryText.Dispose ();
				ExportDirectoryText = null;
			}

			if (ExportFormatCombobox != null) {
				ExportFormatCombobox.Dispose ();
				ExportFormatCombobox = null;
			}

			if (FindDatabaseFileText != null) {
				FindDatabaseFileText.Dispose ();
				FindDatabaseFileText = null;
			}

			if (FindDeveloperText != null) {
				FindDeveloperText.Dispose ();
				FindDeveloperText = null;
			}

			if (FindInstrumentText != null) {
				FindInstrumentText.Dispose ();
				FindInstrumentText = null;
			}

			if (FindProductText != null) {
				FindProductText.Dispose ();
				FindProductText = null;
			}

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

			if (ProgressBar != null) {
				ProgressBar.Dispose ();
				ProgressBar = null;
			}

		}
	}
}

using System;
using AppKit;
using Foundation;

namespace KeySwitchManager.Xamarin.Mac
{
    public partial class ViewController : NSViewController
    {
        public ViewController( IntPtr handle ) : base( handle ) {}

        #region Application LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
        }

        public override NSObject RepresentedObject
        {
            get { return base.RepresentedObject; }
            set
            {
                base.RepresentedObject = value;
                // Update the view, if already loaded.
            }
        }
        #endregion

        #region UI Event Handlers
        partial void OnOpenNewFileButtonClicked( Foundation.NSObject sender )
        {
            var selectedFile = ChooseSaveFilePath( "yaml", "xlsx" );
        }
        #endregion

        #region Utilities
        private string ChooseOpenFilePath( params string[] extensions )
        {
            var result = string.Empty;

            var dialog = new NSOpenPanel()
            {
                CanChooseDirectories = false,
                AllowsMultipleSelection = false,
                AllowedFileTypes = extensions
            };

            dialog.Begin( ( num ) => {
                if( num == (long)NSModalResponse.OK )
                {
                    result = dialog.Filenames[ 0 ];
                }
            });

            return result;
        }

        private string ChooseSaveFilePath( params string[] extensions )
        {
            var result = string.Empty;

            var dialog = new NSSavePanel()
            {
                CanCreateDirectories    = true,
                AllowsOtherFileTypes    = false,
                AllowedFileTypes        = extensions
            };

            dialog.Begin( ( num ) => {
                if( num == (long)NSModalResponse.OK )
                {
                    result = dialog.Filename;
                }
            });

            return result;
        }
        #endregion
    }
}

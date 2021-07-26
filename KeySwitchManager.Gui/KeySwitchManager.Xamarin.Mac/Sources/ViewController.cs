using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using AppKit;

using Foundation;

using KeySwitchManager.Applications.Core.Controllers;
using KeySwitchManager.Applications.Core.Controllers.Create;
using KeySwitchManager.Applications.Core.Controllers.Export;
using KeySwitchManager.Xamarin.Mac.UiKitView;

using RkHelper.Text;

namespace KeySwitchManager.Xamarin.Mac
{
    public partial class ViewController : NSViewController
    {
        private LogTextView LogView { get; set; }
        private IList<ExportSupportedFormat> ExportSupportedFormatList { get; set; }

        #region Ctor
#pragma warning disable 8618
        public ViewController( IntPtr handle ) : base( handle ) {}
#pragma warning restore 8618
        #endregion

        #region Application LifeCycle
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            // Do any additional setup after loading the view.
            LogView = new LogTextView( LogTextView, this );
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

        #region Executions
        private void PreExecuteController()
        {
            InvokeOnMainThread( () => {
                LogView.Clear();
                // ProgressBar.IsIndeterminate = true;
                // LogClearButton.IsEnabled    = false;
                // MainTabPanel.IsEnabled      = false;
            } );
        }

        private void PostExecuteController()
        {
            InvokeOnMainThread( () => {
                //ProgressBar.IsIndeterminate = false;

                var alert = new NSAlert()
                {
                    AlertStyle  = NSAlertStyle.Informational,
                    MessageText = "Done",
                };
                alert.RunModal();

                // LogClearButton.IsEnabled = true;
                // MainTabPanel.IsEnabled   = true;
            } );
        }

        private async Task ExecuteControllerAsync( Func<IController> controllerFactory )
        {
            PreExecuteController();
            await ControlExecutor.ExecuteAsync( controllerFactory, LogView );
            PostExecuteController();
        }
        #endregion

        #region UI Event Handlers
        #region New
        partial void OnCreateDefinitionFileChooserButtonClicked( NSObject sender )
        {
            ChooseSaveFilePath( ( path ) => {
                NewFileTextField.StringValue = path;
            }, "yaml", "xlsx" );
        }

        async partial void OnOpenNewFileButtonClicked( NSObject sender )
        {
            var path = NewFileTextField.StringValue;

            if( StringHelper.IsEmpty( path ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => CreateControllerFactory.Create( path, LogView ) );
        }

        #endregion
        #endregion

        #region Utilities
        private void ChooseOpenFilePath( Action<string> complete, params string[] extensions )
        {
            var dialog = new NSOpenPanel()
            {
                CanChooseDirectories = false,
                AllowsMultipleSelection = false,
                AllowedFileTypes = extensions
            };

            dialog.Begin( ( num ) => {
                if( num == (long)NSModalResponse.OK )
                {
                    complete.Invoke( dialog.Filenames[ 0 ] );
                }
            });
        }

        private void ChooseSaveFilePath( Action<string> complete, params string[] extensions )
        {
            var dialog = new NSSavePanel()
            {
                CanCreateDirectories    = true,
                AllowsOtherFileTypes    = false,
                AllowedFileTypes        = extensions
            };

            dialog.Begin( ( num ) => {
                if( num == (long)NSModalResponse.OK )
                {
                    complete.Invoke( dialog.Filename );
                }
            });
        }
        #endregion
    }
}

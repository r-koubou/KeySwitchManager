using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using AppKit;

using Foundation;

using KeySwitchManager.Applications.Core.Controllers;
using KeySwitchManager.Applications.Core.Controllers.Create;
using KeySwitchManager.Applications.Core.Controllers.Export;
using KeySwitchManager.Applications.Core.Controllers.Find;
using KeySwitchManager.Applications.Core.Controllers.Import;
using KeySwitchManager.Xamarin.Mac.UiKitView;

using RkHelper.Enumeration;
using RkHelper.Text;

namespace KeySwitchManager.Xamarin.Mac
{
    public partial class ViewController : NSViewController
    {
        private LogTextView LogView { get; set; }

        private IList<ExportSupportedFormat> ExportSupportedFormatList { get; }
            = new List<ExportSupportedFormat>( EnumHelper.GetValues<ExportSupportedFormat>() );

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

            // Format combobox
            ExportFormatCombobox.RemoveAllItems();
            foreach( var f in ExportSupportedFormatList )
            {
                var menu = new NSMenuItem( f.ToString() )
                {
                    Target = this
                };
                ExportFormatCombobox.Menu?.AddItem( menu );
            }
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
                ProgressBar.StartAnimation( this );
                SetEnableViews( TabView, false );
                // LogClearButton.IsEnabled    = false;
                // MainTabPanel.IsEnabled      = false;
            } );
        }

        private void PostExecuteController()
        {
            InvokeOnMainThread( () => {

                ProgressBar.StopAnimation( this );

                var alert = new NSAlert()
                {
                    AlertStyle  = NSAlertStyle.Informational,
                    MessageText = "Done",
                };
                alert.RunModal();

                SetEnableViews( TabView, true );

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
        partial void OnLogClearButtonClicked( NSObject sender )
        {
            LogView.Clear();
        }

        #region New
        partial void OnCreateDefinitionFileChooserButtonClicked( NSObject sender )
        {
            ChooseSaveFilePath( ( path ) => {
                NewFileText.StringValue = path;
            }, "yaml", "xlsx" );
        }

        async partial void OnOpenNewFileButtonClicked( NSObject sender )
        {
            var path = NewFileText.StringValue;

            if( StringHelper.IsEmpty( path ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => CreateControllerFactory.Create( path, LogView ) );
        }

        #endregion

        #region Import
        partial void OnOpenDatabaseFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                ImportDatabaseFileText.StringValue = path;
            }, "db" );
        }

        partial void OnOpenFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                ImportFileText.StringValue = path;
            }, "yaml", "xlsx" );
        }

        async partial void OnDoImportButtonClicked( NSObject sender )
        {
            var databasePath = ImportDatabaseFileText.StringValue;
            var importFilePath = ImportFileText.StringValue;

            if( StringHelper.IsEmpty( databasePath, importFilePath ) )
            {
                return;
            }
            await ExecuteControllerAsync( () => ImportControllerFactory.Create( databasePath, importFilePath, LogView ) );
        }

        #endregion

        #region Find
        partial void OpenFindDatabaseFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                FindDatabaseFileText.StringValue = path;
            }, "db" );
        }

        async partial void OnFindButtonClicked( NSObject sender )
        {
            var databasePath = FindDatabaseFileText.StringValue;
            var developer = FindDeveloperText.StringValue;
            var product = FindProductText.StringValue;
            var instrument = FindInstrumentText.StringValue;

            if( StringHelper.IsEmpty( databasePath, developer, product, instrument ) )
            {
                return;
            }

            if( !File.Exists( databasePath ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => FindControllerFactory.Create( databasePath, developer, product, instrument, LogView ) );
        }


        #endregion

        #region Export
        partial void OnSaveExportDirectoryChooserButtonClicked( NSObject sender )
        {
            ChooseDirectoryPath( ( path ) => {
                ExportDirectoryText.StringValue = path;
            });
        }
        async partial void OnExportButtonClicked( NSObject sender )
        {
            var databasePath = FindDatabaseFileText.StringValue;
            var developer = FindDeveloperText.StringValue;
            var product = FindProductText.StringValue;
            var instrument = FindInstrumentText.StringValue;
            var output = ExportDirectoryText.StringValue;

            var comboboxIndex = Convert.ToInt32( ExportFormatCombobox.IndexOfSelectedItem );

            if( comboboxIndex < 0 )
            {
                return;
            }

            var format = ExportSupportedFormatList[ comboboxIndex ];

            if( StringHelper.IsEmpty( databasePath, developer, product ) )
            {
                return;
            }

            if( !File.Exists( databasePath ) )
            {
                return;
            }

            // Append a sub folder by format name
            output = Path.Combine( output, format.ToString() );

            await ExecuteControllerAsync(
                () => ExportControllerFactory.Create(
                    developer,
                    product,
                    instrument,
                    databasePath,
                    output,
                    format,
                    LogView
                )
            );
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

            if( dialog.RunModal() == (long)NSModalResponse.OK )
            {
                complete.Invoke( dialog.Filenames[ 0 ] );
            }
        }

        private void ChooseSaveFilePath( Action<string> complete, params string[] extensions )
        {
            var dialog = new NSSavePanel()
            {
                CanCreateDirectories    = true,
                AllowsOtherFileTypes    = false,
                AllowedFileTypes        = extensions
            };

            if( dialog.RunModal() == (long)NSModalResponse.OK )
            {
                complete.Invoke( dialog.Filename );
            }
        }

        private void ChooseDirectoryPath( Action<string> complete )
        {
            var dialog = new NSOpenPanel()
            {
                CanCreateDirectories    = true,
                CanChooseFiles          = false,
                CanChooseDirectories    = true,
                AllowsMultipleSelection = false,
            };

            if( dialog.RunModal() == (long)NSModalResponse.OK )
            {
                complete.Invoke( dialog.Filenames[ 0 ] );
            }
        }


        private static void SetEnableViews( NSView view, bool enabled )
        {
            foreach( var v in view.Subviews )
            {
                if( v is NSControl c )
                {
                    c.Enabled = enabled;
                }
                else
                {
                    SetEnableViews( v, enabled );
                }
            }
        }

        #endregion
    }
}

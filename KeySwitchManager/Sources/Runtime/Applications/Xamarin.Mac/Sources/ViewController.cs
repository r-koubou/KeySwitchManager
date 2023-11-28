using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using AppKit;

using CoreGraphics;

using Foundation;

using KeySwitchManager.Applications.Standalone.Core.Controllers;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Create;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Delete;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Export;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Find;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Import;
using KeySwitchManager.Applications.Standalone.Core.Presenters;
using KeySwitchManager.Xamarin.Mac.UiKitView;

using RkHelper.Enumeration;
using RkHelper.Primitives;

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

            await Task.Run( async () => {
                    await ControlExecutor.ExecuteAsync( controllerFactory, LogView );
                }
            );

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
            }, "yaml", "yml", "xlsx" );
        }

        async partial void OnOpenNewFileButtonClicked( NSObject sender )
        {
            var path = NewFileText.StringValue;

            if( StringHelper.IsEmpty( path ) )
            {
                return;
            }

            ICreateControllerFactory factory = new CreateFileControllerFactory();
            var presenter = new CreatePresenter( LogView );

            await ExecuteControllerAsync( () => factory.Create( path, presenter ) );
        }

        #endregion

        #region Import
        partial void OnOpenDatabaseFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                ImportDatabaseFileText.StringValue = path;
            }, "yaml", "yml" );
        }

        partial void OnOpenFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                ImportFileText.StringValue = path;
            }, "yaml", "yml", "xlsx" );
        }

        async partial void OnDoImportButtonClicked( NSObject sender )
        {
            var databasePath = ImportDatabaseFileText.StringValue;
            var importFilePath = ImportFileText.StringValue;

            if( StringHelper.IsEmpty( databasePath, importFilePath ) )
            {
                return;
            }

            IImportControllerFactory importControllerFactory = new ImportControllerFactory( LogView );
            await ExecuteControllerAsync( () => importControllerFactory.Create( databasePath, importFilePath ) );
        }

        #endregion

        #region Find
        partial void OpenFindDatabaseFileChooserButtonClicked( NSObject sender )
        {
            ChooseOpenFilePath( ( path ) => {
                FindDatabaseFileText.StringValue = path;
            }, "yaml", "yml" );
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

        #region Delete
        async partial void OnDeleteButtonClicked( NSObject sender )
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

            if( !ChooseYesNoDialog( "Warning", "Are you sure?" ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => DeleteControllerFactory.Create( databasePath, developer, product, instrument, LogView ) );

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

            IExportControllerFactory controllerFactory = new ExportFileControllerFactory( databasePath, output );

            await ExecuteControllerAsync(
                () => controllerFactory.Create(
                    developer,
                    product,
                    instrument,
                    format,
                    LogView
                )
            );
        }

        #endregion

        #endregion

        #region Utilities
        private bool ChooseYesNoDialog( string title, string message )
        {
            var dialog = new NSAlert
            {
                AlertStyle      = NSAlertStyle.Warning,
                MessageText     = title,
                InformativeText = message
            };

            dialog.AddButton( "Yes" );
            dialog.AddButton( "No" );

            return dialog.RunModal() == (long)NSAlertButtonReturn.First;
        }

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
                complete.Invoke( dialog.Filename );
            }
        }

        private void ChooseSaveFilePath( Action<string> complete, params string[] extensions )
        {
            if( extensions == null! || extensions.Length == 0 )
            {
                throw new ArgumentException( nameof( extensions ) );
            }

            var (accessoryView, popup) = CreateFileChooserAccessoryView( extensions );
            var dialog = new NSSavePanel()
            {
                ExtensionHidden      = true,
                ShowsTagField        = false,
                CanCreateDirectories = true,
                AllowsOtherFileTypes = false,
                AllowedFileTypes     = new[] { extensions[ 0 ] }, // 全要素指定はNG。popupで動的に書き換える必要があるため
            };

            if( extensions.Length >= 2 )
            {
                dialog.AccessoryView = accessoryView;
            }

            popup.Activated += ( _, _ ) => {
                dialog.AllowedFileTypes = new[] { extensions[ popup.IndexOfSelectedItem ] };
            };

            if( dialog.RunModal() == (long)NSModalResponse.OK )
            {
                complete.Invoke( dialog.Filename );
            }
        }

        private static(NSView, NSPopUpButton) CreateFileChooserAccessoryView( string[] fileExtensions )
        {
            var accessoryView = new NSView( new CGRect( 0,  0,  200, 50 ) );
            var popup = new NSPopUpButton( new CGRect( 100, 10, 100, 25 ), false );
            var label = new NSTextField( new CGRect( 20,    15, 80,  15 ) );

            popup.AddItems( fileExtensions );

            label.StringValue     = "Format: ";
            label.Alignment       = NSTextAlignment.Center;
            label.Bordered        = false;
            label.Selectable      = false;
            label.Editable        = false;
            label.BackgroundColor = NSColor.Clear;
            label.TextColor       = NSColor.Label;

            accessoryView.AddSubview( popup );
            accessoryView.AddSubview( label );

            return ( accessoryView, popup );
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

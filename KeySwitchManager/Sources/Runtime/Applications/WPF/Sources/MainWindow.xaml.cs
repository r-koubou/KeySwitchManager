using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading.Tasks;
using System.Windows;

using KeySwitchManager.Applications.Core.Controllers;
using KeySwitchManager.Applications.Core.Controllers.Create;
using KeySwitchManager.Applications.Core.Controllers.Delete;
using KeySwitchManager.Applications.Core.Controllers.Export;
using KeySwitchManager.Applications.Core.Controllers.Find;
using KeySwitchManager.Applications.Core.Controllers.Import;
using KeySwitchManager.WPF.WpfView;

using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

using RkHelper.Enumeration;
using RkHelper.Text;

using MSAPI = Microsoft.WindowsAPICodePack;

namespace KeySwitchManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string KeySwitchDefinitionFileFilter = "KeySwitch definition File|*.xlsx;*.yaml;*.yml";
        private const string KeySwitchDatabaseFileFilter = "KeySwitch Database File|*.yaml;*.yml;*.db";
        private const string KeySwitchAllFileFilter = "Supported File|*.xlsx;*.yaml;*.yml;*.db";

        #region UI Binding
        #endregion

        private LogTextView LogTextView { get; }
        private IList<ExportSupportedFormat> ExportSupportedFormatList { get; }

        public MainWindow()
        {
            InitializeComponent();

            LogTextView               = new LogTextView( LogTextBox, this.Dispatcher );
            ExportSupportedFormatList = new List<ExportSupportedFormat>( Enum.GetValues<ExportSupportedFormat>() );

            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            ExportFormatCombobox.ItemsSource = ExportSupportedFormatList;
            LoadApplicationConfig();
        }

        #region Executions
        private void PreExecuteController()
        {
            Dispatcher.Invoke( () => {
                LogTextView.Clear();
                ProgressBar.IsIndeterminate = true;
                LogClearButton.IsEnabled    = false;
                MainTabPanel.IsEnabled      = false;
            } );
        }

        private void PostExecuteController()
        {
            Dispatcher.Invoke( () => {
                ProgressBar.IsIndeterminate = false;

                MessageBox.Show( "Done" );
                LogClearButton.IsEnabled = true;
                MainTabPanel.IsEnabled   = true;
            } );
        }

        private async Task ExecuteControllerAsync( Func<IController> controllerFactory )
        {
            PreExecuteController();
            await Task.Run( async () => await ControlExecutor.ExecuteAsync( controllerFactory, LogTextView ) );
            PostExecuteController();
        }
        #endregion

        #region Utilities
        private string ChooseDirectoryPath( string title )
        {
            // https://qiita.com/Kosen-amai/items/9de7a77a1e6b7851a0b3

            var dialog = new CommonOpenFileDialog();

            dialog.IsFolderPicker = true;
            dialog.Title = title;

            if( dialog.ShowDialog() == CommonFileDialogResult.Ok )
            {
                return dialog.FileName;
            }

            return string.Empty;
        }

        private string ChooseOpenFilePath( string filter )
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter
            };

            if( dialog.ShowDialog() == true )
            {
                return dialog.FileName;
            }

            return string.Empty;
        }
        private string ChooseSaveFilePath(string filter)
        {
            var dialog = new SaveFileDialog
            {
                Filter = filter
            };

            if (dialog.ShowDialog() == true)
            {
                return dialog.FileName;
            }

            return string.Empty;
        }

        private static void HandleDnDFilePreviewDragOver( DragEventArgs e )
        {
            e.Effects = DragDropEffects.None;

            if( e.Data.GetDataPresent( DataFormats.FileDrop, true ) )
            {
                e.Effects = DragDropEffects.Copy;
            }

            e.Handled = true;
        }

        private static void HandleDnDFileDrop( DragEventArgs e, Action<List<string>> resultPathList )
        {
            if( e.Data.GetData( DataFormats.FileDrop ) is not string[] files )
            {
                return;
            }

            resultPathList( new List<string>( files ) );
        }
        #endregion

        #region UI Event handlers
        private void OnLogClearButtonClick( object sender, RoutedEventArgs e )
        {
            LogTextBox.Text = string.Empty;
        }

        #region New
        private async void OnDoCreateNewDefinitionButtonClicked( object sender, RoutedEventArgs e )
        {
            var path = NewFileText.Text;

            if( StringHelper.IsEmpty( path ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => CreateControllerFactory.Create( path, LogTextView ) );
        }
        #endregion

        #region Import
        private void OnOpenDatabaseFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportDatabaseFileText.Text = ChooseSaveFilePath( KeySwitchDatabaseFileFilter );
        }

        private void OnOpenFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportFileText.Text = ChooseOpenFilePath( KeySwitchDefinitionFileFilter );
        }

        private async void OnDoImportButtonClick( object sender, RoutedEventArgs e )
        {
            var databasePath = ImportDatabaseFileText.Text;
            var importFilePath = ImportFileText.Text;

            if( StringHelper.IsEmpty( databasePath, importFilePath ) )
            {
                return;
            }
            await ExecuteControllerAsync( () => ImportControllerFactory.Create( databasePath, importFilePath, LogTextView ) );
        }
        #endregion

        #region Find
        private void OnCreateDefinitionFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            NewFileText.Text = ChooseSaveFilePath( KeySwitchAllFileFilter );
        }

        private void OpenFindDatabaseFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            FindDatabaseFileText.Text = ChooseOpenFilePath( KeySwitchDatabaseFileFilter );
        }

        private async void OnFindButtonClicked( object sender, RoutedEventArgs e )
        {
            var databasePath = FindDatabaseFileText.Text;
            var developer = FindDeveloperText.Text;
            var product = FindProductText.Text;
            var instrument = FindInstrumentText.Text;

            if( StringHelper.IsEmpty( databasePath, developer, product, instrument ) )
            {
                return;
            }

            if( !File.Exists( databasePath ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => FindControllerFactory.Create( databasePath, developer, product, instrument, LogTextView ) );
        }

        #endregion

        #region Delete
        private async void OnDeleteButtonClicked( object sender, RoutedEventArgs e )
        {
            if( MessageBox.Show( "Are you sure?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning ) != MessageBoxResult.Yes )
            {
                return;
            }

            var databasePath = FindDatabaseFileText.Text;
            var developer = FindDeveloperText.Text;
            var product = FindProductText.Text;
            var instrument = FindInstrumentText.Text;

            if( StringHelper.IsEmpty( databasePath, developer, product, instrument ) )
            {
                return;
            }

            if( !File.Exists( databasePath ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => DeleteControllerFactory.Create( databasePath, developer, product, instrument, LogTextView ) );

        }
        #endregion

        #region Export

        private void OnSaveExportDirectoryChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ExportDirectoryText.Text = ChooseDirectoryPath( "Choose Export Directory" );
        }

        private async void OnExportButtonClicked(object sender, RoutedEventArgs e)
        {
            var databasePath = FindDatabaseFileText.Text;
            var developer = FindDeveloperText.Text;
            var product = FindProductText.Text;
            var instrument = FindInstrumentText.Text;
            var output = ExportDirectoryText.Text;

            var format = ExportSupportedFormatList[ ExportFormatCombobox.SelectedIndex ];

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
                    LogTextView
                )
            );
        }


        #endregion

        #region Textbox D&D
        private void OnImportDatabaseFileTextPreviewDragOver( object sender, DragEventArgs e )
        {
            HandleDnDFilePreviewDragOver( e );
        }

        private void OnImportDatabaseFileTextDrop( object sender, DragEventArgs e )
        {
            HandleDnDFileDrop( e, ( files ) => {
                ImportDatabaseFileText.Text = files[ 0 ];
            });
        }

        private void OnImportFileTextPreviewDragOver( object sender, DragEventArgs e )
        {
            HandleDnDFilePreviewDragOver( e );
        }

        private void OnImportFileTextDrop( object sender, DragEventArgs e )
        {
            HandleDnDFileDrop( e, ( files ) => {
                ImportFileText.Text = files[ 0 ];
            });
        }

        private void OnFindDatabaseFileTextPreviewDragOver( object sender, DragEventArgs e )
        {
            HandleDnDFilePreviewDragOver( e );
        }

        private void OnFindDatabaseFileTextDrop( object sender, DragEventArgs e )
        {
            HandleDnDFileDrop( e, ( files ) => {
                FindDatabaseFileText.Text = files[ 0 ];
            });
        }
        #endregion
        #endregion

        #region Window Event Handling
        protected override void OnClosing( CancelEventArgs e )
        {
            SaveApplicationConfig();
            base.OnClosing( e );
        }
        #endregion

        private void LoadApplicationConfig()
        {
            Dispatcher.Invoke( () => {
                    var config = ApplicationConfig.Load();
                    ImportDatabaseFileText.Text        = config.ImportDatabasePath;
                    FindDatabaseFileText.Text          = config.ExportDatabasePath;
                    ExportDirectoryText.Text           = config.ExportDirectory;
                    ExportFormatCombobox.SelectedIndex = (int)EnumHelper.Parse( config.ExportFormat, ExportSupportedFormat.Xlsx );
                }
            );
        }

        private void SaveApplicationConfig()
        {
            Dispatcher.Invoke( () => {
                    var config = new ApplicationConfigModel
                    {
                        ImportDatabasePath = ImportDatabaseFileText.Text,
                        ExportDatabasePath = FindDatabaseFileText.Text,
                        ExportDirectory    = ExportDirectoryText.Text,
                        ExportFormat       = ExportSupportedFormatList[ ExportFormatCombobox.SelectedIndex ].ToString()
                    };

                    ApplicationConfig.Save( config );
                }
            );
        }
    }
}

﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using KeySwitchManager.Applications.Standalone.KeySwitches;
using KeySwitchManager.Applications.Standalone.KeySwitches.Controllers.Extensions;
using KeySwitchManager.Applications.WPF.WpfView;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;

using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

using RkHelper.Enumeration;
using RkHelper.Primitives;

namespace KeySwitchManager.Applications.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private const string KeySwitchDefinitionFileFilter = "KeySwitch definition File|*.xlsx;*.yaml;*.yml";
        private const string KeySwitchDatabaseFileFilter = "KeySwitch Database File|*.yaml;*.yml";
        private const string KeySwitchAllFileFilter = "Supported File|*.xlsx;*.yaml;*.yml";

        #region UI Binding
        #endregion

        private LogTextView LogView { get; }
        private IList<ExportFormat> ExportSupportedFormatList { get; }

        public MainWindow()
        {
            InitializeComponent();

            LogView               = new LogTextView( LogTextBox, this.Dispatcher );
            ExportSupportedFormatList = new List<ExportFormat>( Enum.GetValues<ExportFormat>() );

            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
            ExportFormatCombobox.ItemsSource = ExportSupportedFormatList;
            InitializeWindowTitle();
            LoadApplicationConfig();
        }

        private void InitializeWindowTitle()
        {
            var version = typeof( MainWindow ).Assembly.GetName().Version;

            if( version != null )
            {
                Title = $"{Title} (Version {version.ToString( 3 )})";
            }

        }

        #region Executions
        private void PreExecuteController()
        {
            Dispatcher.Invoke( () => {
                LogView.Clear();
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

        private async Task ExecuteControllerAsync( Func<Task> execute )
        {
            PreExecuteController();

            await Task.Run( async () =>
                {
                    await execute();
                }
            );

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

            var controller = new CreateController();
            var presenter = new CreatePresenter( LogView );

            await ExecuteControllerAsync( async () => await controller.CreateToLocalFileAsync( path, presenter, CancellationToken.None ) );
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

            var controller = new ImportController();
            var presenter = new ImportPresenter( LogView );

            await ExecuteControllerAsync( async () => await controller.ExecuteAsync( databasePath, importFilePath, presenter, CancellationToken.None ) );
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

            var controller = new FindController();
            var presenter = new FindPresenter( LogView );

            await ExecuteControllerAsync( async () => await controller.ExecuteAsync( databasePath, developer, product, instrument, presenter, CancellationToken.None ) );
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

            var controller = new DeleteController();
            var presenter = new DeletePresenter( LogView );

            await ExecuteControllerAsync( async () => await controller.DeleteFromLocalDatabaseAsync( databasePath, developer, product, instrument, presenter, CancellationToken.None ) );
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

            var controller = new ExportController();
            var presenter = new ExportPresenter( LogView );

            await ExecuteControllerAsync( async () => await controller.ExportToLocalFileAsync( databasePath, developer, product, instrument, output, format, presenter, CancellationToken.None ) );
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
                    ExportFormatCombobox.SelectedIndex = (int)EnumHelper.Parse( config.ExportFormat, ExportFormat.Xlsx );
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

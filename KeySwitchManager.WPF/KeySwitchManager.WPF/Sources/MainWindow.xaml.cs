using System;
using System.Threading.Tasks;
using System.Windows;

using KeySwitchManager.GuiCore.Sources.Controllers;
using KeySwitchManager.GuiCore.Sources.Controllers.Create;
using KeySwitchManager.GuiCore.Sources.Controllers.Import;
using KeySwitchManager.WPF.WpfView;

using Microsoft.Win32;

using RkHelper.Text;

namespace KeySwitchManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        #region UI Binding
        #endregion

        private LogTextView LogTextView { get; }

        public MainWindow()
        {
            InitializeComponent();

            LogTextView = new LogTextView( LogTextBox, this.Dispatcher );
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {}

        #region Executions
        private void PreExecuteController()
        {
            Dispatcher.Invoke( () => {
                LogTextView.Clear();
                ProgressBar.IsIndeterminate           = true;
                DoCreateNewDefinitionButton.IsEnabled = false;
                LogClearButton.IsEnabled              = false;
            } );
        }

        private void PostExecuteController()
        {
            Dispatcher.Invoke( () => {
                ProgressBar.IsIndeterminate = false;

                MessageBox.Show( "Done" );
                DoCreateNewDefinitionButton.IsEnabled = true;
                LogClearButton.IsEnabled              = true;
            } );
        }

        private async Task ExecuteControllerAsync( Func<IController> controllerFactory )
        {
            PreExecuteController();
            await ControlExecutor.ExecuteAsync( controllerFactory, LogTextView );
            PostExecuteController();
        }
        #endregion

        #region Utilities
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
        #endregion

        #region UI Event handlers
        private void OnLogClearButtonClick( object sender, RoutedEventArgs e )
        {
            LogTextBox.Text = string.Empty;
        }

        private async void OnDoCreateNewDefinitionButtonClicked( object sender, RoutedEventArgs e )
        {
            var path = NewFileText.Text;

            if( StringHelper.IsEmpty( path ) )
            {
                return;
            }

            await ExecuteControllerAsync( () => CreateControllerFactory.Create( path, LogTextView ) );
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

        private void OnOpenDatabaseFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportDatabaseFileText.Text = ChooseOpenFilePath( "Database File|*.db|All Types|*" );
        }

        private void OnOpenFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportFileText.Text = ChooseOpenFilePath( "KeySwitch definition File|*.yaml;*.xlsx" );
        }

        private void OnCreateDefinitionFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            NewFileText.Text = ChooseSaveFilePath( "KeySwitch definition File|*.xlsx|KeySwitch definition File|*.yaml" );
        }
        #endregion
    }
}

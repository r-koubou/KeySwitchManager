using System;
using System.Threading.Tasks;
using System.Windows;

using KeySwitchManager.GuiCore.Sources.Controllers;
using KeySwitchManager.GuiCore.Sources.Controllers.Create;
using KeySwitchManager.GuiCore.Sources.Controllers.Import;
using KeySwitchManager.GuiCore.Sources.View.LogView;
using KeySwitchManager.WPF.WpfView;

using Microsoft.Win32;

namespace KeySwitchManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region UI Binding
        #endregion

        private LogView LogView { get; }

        public MainWindow()
        {
            InitializeComponent();

            LogView = new LogView( LogTextBox, this.Dispatcher );
            InitializeCustomComponent();
        }

        private void InitializeCustomComponent()
        {
        }

        #region Executions
        private void PreExecuteControllerInMainThread()
        {
            ProgressBar.IsIndeterminate           = true;
            DoCreateNewDefinitionButton.IsEnabled = false;
            LogClearButton.IsEnabled              = false;
        }

        private void PostExecuteControllerInMainThread()
        {
            ProgressBar.IsIndeterminate           = false;
            DoCreateNewDefinitionButton.IsEnabled = true;
            LogClearButton.IsEnabled              = true;
        }

        private async Task ExecuteControllerAsync(  Func<IController> controllerFactory )
        {
            try
            {
                PreExecuteControllerInMainThread();

                var controller = controllerFactory.Invoke();

                await ExecuteControllerAsyncImpl( controller );
                controller.Dispose();
            }
            catch( Exception exception )
            {
                LogView.Append( new LogViewModel( exception.ToString() ) );
            }
            finally
            {
                MessageBox.Show( "Done" );
                PostExecuteControllerInMainThread();
            }

        }

        private async Task ExecuteControllerAsyncImpl( IController controller )
        {
            var dispatcher = Dispatcher;

            await Task.Run( () =>
            {
                try
                {
                    controller.Execute();
                }
                catch( Exception e )
                {
                    dispatcher.Invoke( () =>
                    {
                        LogView.Append( new LogViewModel( e.ToString() ) );
                    });
                }
            });
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
            await ExecuteControllerAsync( () => CreateControllerFactory.Create( NewFileText.Text, LogView ) );
        }

        private async void OnDoImportButtonClick( object sender, RoutedEventArgs e )
        {
            await ExecuteControllerAsync( () => ImportControllerFactory.Create( ImportDatabaseFileText.Text, ImportFileText.Text, LogView ) );
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

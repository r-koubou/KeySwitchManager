using System;
using System.Threading.Tasks;
using System.Windows;

using KeySwitchManager.GuiCore.Sources.Controllers;
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
        private LogView LogView { get; }

        public MainWindow()
        {
            InitializeComponent();
            LogView = new LogView( LogTextBox, this.Dispatcher );
        }


        #region Executions
        private async Task ExecuteImportAsync( IController controller )
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
        private string ChooseFilePath( string filter )
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
        #endregion

        #region UI Event handlers
        private void OnLogClearButtonClick( object sender, RoutedEventArgs e )
        {
            LogTextBox.Text = string.Empty;
        }

        private async void OnDoImportButtonClick( object sender, RoutedEventArgs e )
        {
            try
            {
                using var controller = ImportControllerFactory.Create( ImportDatabaseFileText.Text, ImportFileText.Text, LogView );
                await ExecuteImportAsync( controller );
                controller.Dispose();
                MessageBox.Show( "Done" );
            }
            catch( Exception exception )
            {
                LogView.Append( new LogViewModel( exception.ToString() ) );
            }
        }

        private void OnOpenDatabaseFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportDatabaseFileText.Text = ChooseFilePath( "Database File|*.db|All Types|*" );
        }

        private void OnOpenFileChooserButtonClicked( object sender, RoutedEventArgs e )
        {
            ImportFileText.Text = ChooseFilePath("KeySwitch definition File|*.yaml;*.xlsx");
        }
        #endregion
    }
}

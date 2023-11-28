using KeySwitchManager.Applications.Standalone.Core.Helpers;
using KeySwitchManager.Applications.Standalone.Core.Presenters;
using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Delete
{
    public static class DeleteControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new DeletePresenter( logTextView );

            return new DeleteController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

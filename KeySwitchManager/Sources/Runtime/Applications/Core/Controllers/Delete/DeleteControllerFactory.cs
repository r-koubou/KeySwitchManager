using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Delete
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

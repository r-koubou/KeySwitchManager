using KeySwitchManager.Applications.Core.Helpers;
using KeySwitchManager.Applications.Core.Views.LogView;

namespace KeySwitchManager.Applications.Core.Controllers.Find
{
    public static class FindControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new FindPresenter( logTextView );

            return new FindController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

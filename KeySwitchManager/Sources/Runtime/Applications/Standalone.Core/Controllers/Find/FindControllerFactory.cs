using KeySwitchManager.Applications.Standalone.Core.Helpers;
using KeySwitchManager.Applications.Standalone.Core.Presenters;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Find
{
    public class FindControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new FindPresenter( logTextView );

            return new FindController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Delete
{
    public class DeleteControllerFactory : IDeleteControllerFactory
    {
        public IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var presenter = new DeletePresenter( logTextView );

            return new DeleteController( databaseRepository, presenter, developer, product, instrument );
        }


    }
}

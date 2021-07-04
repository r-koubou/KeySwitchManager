using KeySwitchManager.Commons.Data;
using KeySwitchManager.Core.Applications.Views.LogView;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;

namespace KeySwitchManager.Core.Applications.Controllers.Delete
{
    public static class DeleteControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
            var presenter = new DeletePresenter( logTextView );

            return new DeleteController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

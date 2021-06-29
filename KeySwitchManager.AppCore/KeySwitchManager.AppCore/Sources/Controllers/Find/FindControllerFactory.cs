using KeySwitchManager.AppCore.View.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;

namespace KeySwitchManager.AppCore.Controllers.Find
{
    public static class FindControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
            var presenter = new FindPresenter( logTextView );

            return new FindController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

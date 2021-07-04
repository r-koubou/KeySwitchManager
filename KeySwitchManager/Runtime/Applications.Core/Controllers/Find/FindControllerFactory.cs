using KeySwitchManager.Commons.Data;
using KeySwitchManager.Core.Applications.Views.LogView;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;

namespace KeySwitchManager.Core.Applications.Controllers.Find
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

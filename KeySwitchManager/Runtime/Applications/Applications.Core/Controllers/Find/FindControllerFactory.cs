using Application.Core.Views.LogView;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;

namespace Application.Core.Controllers.Find
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

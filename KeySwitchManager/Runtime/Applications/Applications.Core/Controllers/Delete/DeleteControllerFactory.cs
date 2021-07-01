using Application.Core.Views.LogView;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;

namespace Application.Core.Controllers.Delete
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

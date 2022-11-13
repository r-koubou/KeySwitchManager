using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.Applications.Core.Controllers.Delete
{
    public static class DeleteControllerFactory
    {
        public static IController Create( string databasePath, string developer, string product, string instrument, ILogTextView logTextView )
        {
            var databaseRepository = new YamlRepository( new FilePath( databasePath ) );
            var presenter = new DeletePresenter( logTextView );

            return new DeleteController( databaseRepository, presenter, developer, product, instrument );
        }
    }
}

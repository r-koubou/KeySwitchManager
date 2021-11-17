using System.IO;
using System.Text;

using KeySwitchManager.Applications.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Core.Controllers.Dump
{
    public static class DumpFileControllerFactory
    {
        public static IController Create( string databasePath, string outputPath, ILogTextView logTextView )
        {
            var database = new LiteDbKeySwitchRepository( new FilePath( databasePath ) );
            var writer = new YamlKeySwitchWriter( File.Create( outputPath ), Encoding.UTF8, true );

            var presenter = new IDumpFilePresenter.Console();

            var controller = new DumpFileController(
                database,
                writer,
                presenter
            );

            return controller;
        }
    }
}

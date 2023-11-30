using System.IO;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Applications.Standalone.Core.Presenters;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Controllers.KeySwitches.Dump;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Export;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Dump
{
    public class DumpFileControllerFactory : IDumpControllerFactory
    {
        IController IDumpControllerFactory.Create( string databasePath, string outputFilePath, ILogTextView logTextView )
        {
            var database = KeySwitchRepositoryFactory.CreateFileRepository( databasePath );
            var outputDirectory = new DirectoryPath( Path.GetDirectoryName( outputFilePath ) ?? string.Empty );

            var contentFactory       = new YamlExportContentFactory();
            var contentWriterFactory = new YamlExportContentFileWriterFactory( outputDirectory );
            var strategy = new SingleExportStrategy( contentWriterFactory, contentFactory );

            var presenter = new DumpPresenter( logTextView );

            return new DumpController(
                database,
                strategy,
                presenter
            );
        }
    }
}

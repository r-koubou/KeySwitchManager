using System.IO;

using KeySwitchManager.Applications.Standalone.Core.Helpers;
using KeySwitchManager.Applications.Standalone.Core.Views.LogView;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Dump
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

            var presenter = new IDumpFilePresenter.Console();

            return new DumpFileController(
                database,
                strategy,
                presenter
            );
        }
    }
}

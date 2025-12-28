using System.IO;

using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.KeySwitches.Helpers;
using KeySwitchManager.Commons.Data;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Export;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Dump
    {
        [Verb( "dump", HelpText = "search a data from database by given parameter")]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;
            [Option( 'o', "output", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            var database = KeySwitchRepositoryFactory.CreateFileRepository( option.DatabasePath );
            var controller = new DumpController();

            var outputDirectory = new DirectoryPath( Path.GetDirectoryName( option.OutputPath ) ?? string.Empty );

            var contentFactory       = new YamlExportContentFactory();
            var contentWriterFactory = new YamlExportContentFileWriterFactory( outputDirectory );
            var strategy = new SingleExportStrategy( contentWriterFactory, contentFactory );

            controller.Dump(
                database,
                strategy,
                new DumpPresenter( new ConsoleLogView() )
            );

            return 0;
        }
    }
}

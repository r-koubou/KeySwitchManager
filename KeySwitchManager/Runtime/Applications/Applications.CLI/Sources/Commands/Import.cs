using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Text;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Import : ICommand
    {
        [Verb( "import", HelpText = "import a yaml to database")]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", Required = true )]
            public string InputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            using var inputRepository = new YamlKeySwitchFileRepository( new FilePath( option.InputPath ), true );

            var presenter = new IImportTextPresenter.Console();
            var interactor = new ImportTextInteractor( repository, inputRepository, presenter );

            var request = new ImportTextRequest();

            _ = interactor.Execute( request );

            return 0;
        }
    }
}
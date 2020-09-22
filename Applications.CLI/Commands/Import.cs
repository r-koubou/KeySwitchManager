using System.IO;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Interactors.KeySwitches.Importing.Text;
using KeySwitchManager.Json.KeySwitches.Translations;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.Apps.CLI.Commands
{
    public class Import : ICommand
    {
        [Verb( "import", false, HelpText = "import a json to database")]
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

            using var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new JsonModelListToKeySwitchList();
            var presenter = new IImportingTextPresenter.Console();
            var interactor = new ImportingJsonInteractor( repository, translator, presenter );

            var input = new ImportingTextRequest( File.ReadAllText( option.InputPath ) );

            _ = interactor.Execute( input );

            return 0;
        }
    }
}
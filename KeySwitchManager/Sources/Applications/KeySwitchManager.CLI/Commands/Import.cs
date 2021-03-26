using System.Diagnostics.CodeAnalysis;
using System.IO;

using CommandLine;

using Database.LiteDB.KeySwitch.KeySwitch;

using KeySwitchManager.Interactors.KeySwitches.Importing;
using KeySwitchManager.Json.KeySwitch.Translation;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.CLI.Commands
{
    public class Import : ICommand
    {
        [Verb( "import", HelpText = "import a json to database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
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
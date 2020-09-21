using System.IO;

using CommandLine;

using Databases.LiteDB.KeySwitches.KeySwitches;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Value;
using KeySwitchManager.Interactors.KeySwitches.Importing.Text;
using KeySwitchManager.Interactors.KeySwitches.Importing.Xlsx;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;
using KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx;
using KeySwitchManager.Xlsx.KeySwitches.Translators;

namespace KeySwitchManager.Apps.CLI.Commands
{
    public class ImportXlsx : ICommand
    {
        [Verb( "import-xlsx", false, HelpText = "import a xlsx to database")]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Required = true )]
            public string Product { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", Required = true )]
            public string InputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var repository = new LiteDbKeySwitchRepository( option.DatabasePath );
            var translator = new XlsxWorkbookToKeySwitchList( option.Developer, option.Product );
            var presenter = new IImportingXlsxPresenter.Console();
            var interactor = new ImportingXlsxInteractor( repository, translator, presenter );

            var input = new ImportingXlsxRequest( new FilePath( option.InputPath ) );

            _ = interactor.Execute( input );

            return 0;
        }
    }
}
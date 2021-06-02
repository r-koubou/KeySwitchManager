using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.CLI.Commands
{
    public class ImportXlsx : ICommand
    {
        [Verb( "import-xlsx", HelpText = "import a xlsx to database")]
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
            using var inputRepository = new ClosedXmlFileLoadRepository( new FilePath( option.InputPath ) );

            var presenter = new ISpreadsheetImportPresenter.Console();
            var interactor = new SpreadSheetImportInteractor( repository, inputRepository, presenter );

            var request = new SpreadsheetImportRequest();
            _ = interactor.Execute( request );

            return 0;
        }
    }
}
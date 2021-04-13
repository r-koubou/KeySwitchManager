using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Infrastructure.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet;

namespace KeySwitchManager.CLI.Commands
{
    public class ImportXlsx : ICommand
    {
        [Verb( "import-xlsx", HelpText = "import a xlsx to database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'a', "author" )]
            public string Author { get; set; } = string.Empty;

            [Option( 'n', "description" )]
            public string Description { get; set; } = string.Empty;

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

            var info = new KeySwitchInfo(
                option.Developer,
                option.Product,
                option.Author,
                option.Description
            );

            using var repository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            using var inputRepository = new ClosedXmlFileLoadRepository( new FilePath( option.InputPath ), info );

            var presenter = new ISpreadsheetImportPresenter.Console();
            var interactor = new SpreadSheetImportInteractor( repository, inputRepository, presenter );

            var request = new SpreadsheetImportRequest();
            _ = interactor.Execute( request );

            return 0;
        }
    }
}
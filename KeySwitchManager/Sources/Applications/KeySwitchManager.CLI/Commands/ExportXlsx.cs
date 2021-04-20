using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Infrastructures.Database.LiteDB.KeySwitches;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportXlsx : ICommand
    {
        [Verb( "export-xlsx", HelpText = "export a xlsx format from database")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "output-dir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
        }


        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var info = new KeySwitchInfo(
                option.Developer,
                option.Product
            );

            using var inputRepository = new LiteDbKeySwitchRepository( new FilePath( option.DatabasePath ) );
            var keySwitches = SearchHelper.Search( inputRepository, info );

            using var outputRepository = new ClosedXmlFileSaveRepository( new DirectoryPath( option.OutputDirectory ) );
            var interactor = new SpreadsheetExportInteractor(
                outputRepository,
                new ISpreadsheetExportPresenter.Console()
            );

            var response = interactor.Execute( new SpreadsheetExportRequest( keySwitches ) );

            return response.Result ? 0 : 1;
        }
    }
}
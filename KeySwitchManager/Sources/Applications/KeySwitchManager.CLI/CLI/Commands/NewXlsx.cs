using System;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate;

namespace KeySwitchManager.CLI.Commands
{
    public class TemplateXlsx : ICommand
    {
        [Verb( "new-xlsx", HelpText = "export a template to xlsx file" )]
        public class CommandOption : ICommandOption
        {
            [Value( index: 0, MetaName = "output", HelpText = "Output path for template file", Default = "(ProductName).xlsx" )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var outputRepository = new ClosedXmlFileSaveTemplateRepository( new FilePath( option.OutputPath ) );
            var interactor = new SpreadsheetTemplateExportInteractor(
                outputRepository,
                new ISpreadsheetTemplateExportPresenter.Console()
            );

            Console.WriteLine( $"generating keyswitch template to {outputRepository.DataPath}" );

            var response = interactor.Execute( new SpreadsheetTemplateExportRequest() );

            return response.Result ? 0 : 1;
        }
    }
}
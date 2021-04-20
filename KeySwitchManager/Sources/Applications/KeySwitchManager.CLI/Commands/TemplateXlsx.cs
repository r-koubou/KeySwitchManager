using System;
using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate;

namespace KeySwitchManager.CLI.Commands
{
    public class TemplateXlsx : ICommand
    {
        [Verb( "template-xlsx", HelpText = "export a template to xlsx file")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {}

        public int Execute( ICommandOption opt )
        {
            using var outputRepository = new ClosedXmlFileSaveTemplateRepository( new FilePath( "(ProductName).xlsx" ) );
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
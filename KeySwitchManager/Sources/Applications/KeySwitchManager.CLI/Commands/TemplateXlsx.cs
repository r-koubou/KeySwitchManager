using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Interactors.KeySwitch.Exporting;
using KeySwitchManager.UseCases.KeySwitch.Exporting;
using KeySwitchManager.Xlsx.KeySwitches.ClosedXml;

namespace KeySwitchManager.CLI.Commands
{
    public class TemplateXlsx : ICommand
    {
        [Verb( "template-xlsx", HelpText = "export a template to xlsx file")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'o', "output-dir", Required = true)]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var repository = new XlsxExportingRepository( new DirectoryPath( option.OutputPath ) );
            var interactor = new ExportingTemplateXlsxInteractor( repository );

            var response = interactor.Execute( new ExportingTemplateXlsxRequest() );

            return response.Result ? 0 : 1;
        }
    }
}
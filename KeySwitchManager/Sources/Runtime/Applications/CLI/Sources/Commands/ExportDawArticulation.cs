using System.IO;

using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Export;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Controllers.KeySwitches.Export;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public abstract class ExportDawArticulation : ICommand
    {
        public class CommandOption : ICommandOption
        {
            [Option( 'q', "quiet" )]
            public bool Quiet { get; set; } = false;

            [Option( 'd', "developer")]
            public string Developer { get; set; } = DeveloperName.Any.Value;

            [Option( 'p', "product")]
            public string Product { get; set; } = ProductName.Any.Value;

            [Option( 'i', "instrument")]
            public string Instrument { get; set; } = InstrumentName.Any.Value;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'o', "outputdir", Required = true )]
            public string OutputDirectory { get; set; } = string.Empty;
        }

        public virtual int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            IExportControllerFactory controllerFactory = new ExportFileControllerFactory(
                option.DatabasePath,
                Path.Combine( option.OutputDirectory, SupportedFormat.ToString() )
            );
            ILogTextView logView = new ConsoleLogView();

            using var controller = controllerFactory.Create(
                option.Developer,
                option.Product,
                option.Instrument,
                SupportedFormat,
                logView
            );

            controller.Execute();

            return 0;
        }

        protected abstract ExportSupportedFormat SupportedFormat { get; }
    }
}

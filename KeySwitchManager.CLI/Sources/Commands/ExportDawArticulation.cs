using System.IO;
using CommandLine;
using KeySwitchManager.Core.Applications.Controllers.Export;
using KeySwitchManager.Core.Applications.Views.LogView;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

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
            var logView = new ConsoleLogView();

            using var controller = ExportControllerFactory.Create(
                option.Developer,
                option.Product,
                option.Instrument,
                option.DatabasePath,
                Path.Combine( option.OutputDirectory, SupportedFormat.ToString() ),
                SupportedFormat,
                logView
            );

            controller.Execute();

            return 0;
        }

        protected abstract ExportSupportedFormat SupportedFormat { get; }
    }
}
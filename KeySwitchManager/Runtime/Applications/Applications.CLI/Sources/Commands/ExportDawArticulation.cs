using System.IO;

using Application.Core.Controllers.Export;
using Application.Core.Views.LogView;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public abstract class ExportDawArticulation : ICommand
    {
        public class CommandOption : ICommandOption
        {
            [Option( 'q', "quiet" )]
            public bool Quiet { get; set; } = false;

            [Option( 'd', "developer", Default = "*")]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Default = "*")]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument", Default = "*")]
            public string Instrument { get; set; } = string.Empty;

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
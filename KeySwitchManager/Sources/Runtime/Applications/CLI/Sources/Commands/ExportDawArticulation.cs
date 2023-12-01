using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Extensions.Controllers;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Presenters.KeySwitches;

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

            using var sourceDatabase = KeySwitchRepositoryFactory.CreateFileRepository( option.DatabasePath );
            var controller = new ExportController();

            controller.Execute(
                option.Developer,
                option.Product,
                option.Instrument,
                option.OutputDirectory,
                Format,
                sourceDatabase,
                new ExportPresenter( new ConsoleLogView() )
            );

            return 0;
        }

        protected abstract ExportFormat Format { get; }
    }
}

using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.Controllers.Find;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Find
    {
        [Verb( "find", HelpText = "search a data from database by given parameter")]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer" )]
            public string Developer { get; set; } = DeveloperName.Any.Value;

            [Option( 'p', "product")]
            public string Product { get; set; } = ProductName.Any.Value;

            [Option( 'i', "instrument" )]
            public string Instrument { get; set; } = InstrumentName.Any.Value;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            using var controller = FindControllerFactory.Create(
                option.DatabasePath,
                option.Developer,
                option.Product,
                option.Instrument,
                new ConsoleLogView()
            );

            controller.Execute();
            return 0;
        }
    }
}

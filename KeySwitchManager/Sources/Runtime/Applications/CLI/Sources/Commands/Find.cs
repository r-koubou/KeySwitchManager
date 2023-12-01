using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Presenters.KeySwitches;

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
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( option.DatabasePath );
            var controller = new FindController();

            controller.Execute(
                option.Developer,
                option.Product,
                option.Instrument,
                repository,
                new FindPresenter( new ConsoleLogView() )
            );

            return 0;
        }
    }
}

using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Dump;
using KeySwitchManager.Controllers.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Dump
    {
        [Verb( "dump", HelpText = "search a data from database by given parameter")]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;
            [Option( 'o', "output", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            IDumpControllerFactory factory = new DumpFileControllerFactory();

            using var controller = factory.Create(
                option.DatabasePath,
                option.OutputPath,
                new ConsoleLogView()
            );

            controller.Execute();
            return 0;
        }
    }
}

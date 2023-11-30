using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Import;
using KeySwitchManager.Controllers.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Import : ICommand
    {
        [Verb( "import", HelpText = "import a yaml or xlsx to database")]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "database", HelpText = "Database file to store", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;

            [Option( 'i', "input", HelpText = "Definition file (yaml, xlsx)", Required = true )]
            public string InputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            IImportControllerFactory factory = new ImportControllerFactory( new ConsoleLogView() );

            using var controller = factory.Create(
                option.DatabasePath,
                option.InputPath
            );

            controller.Execute();

            return 0;
        }
    }
}

using Application.Core.Controllers.Import;
using Application.Core.Views.LogView;

using CommandLine;

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

            using var controller = ImportControllerFactory.Create(
                option.DatabasePath,
                option.InputPath,
                new ConsoleLogView()
            );

            controller.Execute();

            return 0;
        }
    }
}
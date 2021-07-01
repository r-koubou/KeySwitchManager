using Application.Core.Controllers.Find;
using Application.Core.Views.LogView;

using CommandLine;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Find
    {
        [Verb( "find", HelpText = "search a data from database by given parameter")]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true)]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Default = "*" )]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument", Default = "*" )]
            public string Instrument { get; set; } = string.Empty;

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
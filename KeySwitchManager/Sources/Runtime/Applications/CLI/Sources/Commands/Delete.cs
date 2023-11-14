using CommandLine;

using KeySwitchManager.Applications.Standalone.Core.Controllers.Delete;
using KeySwitchManager.Applications.Standalone.Core.Views.LogView;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class Delete : ICommand
    {
        [Verb( "delete", HelpText = "delete records from database" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'd', "developer", Required = true )]
            public string Developer { get; set; } = string.Empty;

            [Option( 'p', "product", Required = true )]
            public string Product { get; set; } = string.Empty;

            [Option( 'i', "instrument", Required = true )]
            public string Instrument { get; set; } = string.Empty;

            [Option( 'f', "database", Required = true )]
            public string DatabasePath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            var logView = new ConsoleLogView();

            logView.Append( $"Developer=\"{option.Developer}\", Product=\"{option.Product}\", Instrument=\"{option.Instrument}\"" );

            using var controller = DeleteControllerFactory.Create( option.DatabasePath, option.Developer, option.Product, option.Instrument, logView );
            controller.Execute();

            return 0;
        }
    }
}

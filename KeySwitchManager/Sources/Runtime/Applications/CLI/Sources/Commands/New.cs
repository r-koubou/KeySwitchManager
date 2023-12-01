using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers;
using KeySwitchManager.Presenters;
using KeySwitchManager.Presenters.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class New : ICommand
    {
        [Verb( "new", HelpText = "export a template file" )]
        public class CommandOption : ICommandOption
        {
            [Value( index: 0, MetaName = "output", HelpText = "Output path for template file (*.yaml or *.xlsx)", Default = "(ProductName).yaml" )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            var logView = new ConsoleLogView();

            var controller = new CreateController();
            logView.Append( $"generating keyswitch template to {option.OutputPath}" );
            controller.Execute( option.OutputPath, new CreatePresenter( new ConsoleLogView() ) );

            return 0;
        }
    }
}

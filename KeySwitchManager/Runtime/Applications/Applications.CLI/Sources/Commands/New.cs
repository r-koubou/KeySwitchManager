using Application.Core.Controllers.Create;
using Application.Core.Views.LogView;

using CommandLine;

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

            using var controller = CreateControllerFactory.Create( option.OutputPath, logView );
            logView.Append( $"generating keyswitch template to {option.OutputPath}" );
            controller.Execute();

            return 0;
        }
    }
}
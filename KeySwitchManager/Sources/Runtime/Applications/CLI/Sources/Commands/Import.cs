using CommandLine;

using KeySwitchManager.Applications.CLI.Views;
using KeySwitchManager.Applications.Standalone.Base.KeySwitches.Helpers;
using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;

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
            using var repository = KeySwitchRepositoryFactory.CreateFileRepository( option.DatabasePath );
            var contentInfo = ImportContentFactory.CreateFromLocalFile( option.InputPath );
            var controller = new ImportController();

            controller.Execute( repository,
                                contentInfo.content, contentInfo.contentReader, new ImportPresenter( new ConsoleLogView() )
            );

            return 0;
        }
    }
}

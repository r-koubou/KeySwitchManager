using System;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.CLI.Commands
{
    public class New : ICommand
    {
        [Verb( "new", HelpText = "export a template generic yaml to file" )]
        public class CommandOption : ICommandOption
        {
            [Value( index: 0, MetaName = "output", HelpText = "Output path for template file", Default = "(ProductName).yaml" )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            var outputPath = option.OutputPath;

            using var outputRepository = new YamlKeySwitchFileRepository( new FilePath( outputPath ), false );

            Console.WriteLine( $"generating keyswitch template to {outputPath}" );

            var interactor = new CreateTextTemplateInteractor( outputRepository );
            _ = interactor.Execute();

            return 0;
        }
    }
}
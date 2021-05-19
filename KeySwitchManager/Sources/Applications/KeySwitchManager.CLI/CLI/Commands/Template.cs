using System;
using System.Diagnostics.CodeAnalysis;

using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.Storage.Yaml.KeySwitches;

namespace KeySwitchManager.CLI.Commands
{
    public class Template : ICommand
    {
        [Verb( "template", HelpText = "export a template generic yaml to file")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {}

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            const string outputPath = "(ProductName).yaml";

            using var outputRepository = new YamlKeySwitchFileRepository( new FilePath( outputPath ), false );

            Console.WriteLine( $"generating keyswitch template to {outputPath}" );

            var interactor = new TemplateKeySwitchCreateInteractor( outputRepository );
            _ = interactor.Execute();

            return 0;
        }
    }
}
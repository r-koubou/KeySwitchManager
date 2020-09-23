using System;
using System.IO;

using CommandLine;

using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.Json.KeySwitches.Translations;

namespace KeySwitchManager.CLI.Commands
{
    public class Template : ICommand
    {
        [Verb( "template", false, HelpText = "export a template generic json to file")]
        public class CommandOption : ICommandOption
        {
            [Option( 'o', "output", Required = true )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var translator = new KeySwitchListListToJsonModelList{ Formatted = true };
            var interactor = new ExportingTemplateJsonInteractor( translator );

            var response = interactor.Execute();

            Console.WriteLine( $"generating json to {option.OutputPath}" );
            File.WriteAllText( option.OutputPath, response.Text );

            return 0;
        }
    }
}
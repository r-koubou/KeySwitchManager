using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using CommandLine;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.Json.KeySwitches.Translations;

namespace KeySwitchManager.CLI.Commands
{
    public class Template : ICommand
    {
        [Verb( "template", HelpText = "export a template generic json to file")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'o', "output" )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var translator = new KeySwitchListListToJsonModelList{ Formatted = true };
            var interactor = new ExportingTemplateJsonInteractor( translator );

            var response = interactor.Execute();

            if( StringHelper.IsNullOrTrimEmpty( option.OutputPath ) )
            {
                Console.Out.WriteLine( response.Text );
            }
            else
            {
                Console.WriteLine( $"generating json to {option.OutputPath}" );
                File.WriteAllText( option.OutputPath, response.Text );
            }

            return 0;
        }
    }
}
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

using CommandLine;

using KeySwitchManager.Interactors.KeySwitches.Exporting;
using KeySwitchManager.Json.KeySwitch.Translation;

using RkHelper.IO;
using RkHelper.Text;

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

            if( StringHelper.IsEmpty( option.OutputPath ) )
            {
                Console.Out.WriteLine( response.Text );
            }
            else
            {
                var outputDirectory = Path.GetDirectoryName( option.OutputPath )!;
                DirectoryHelper.Create( outputDirectory );

                Console.WriteLine( $"generating json to {option.OutputPath}" );
                File.WriteAllText( option.OutputPath, response.Text );
            }

            return 0;
        }
    }
}
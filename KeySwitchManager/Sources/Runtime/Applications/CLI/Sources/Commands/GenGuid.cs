using System;
using System.IO;
using System.Text;

using CommandLine;

using RkHelper.Primitives;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class GenGuid : ICommand
    {
        [Verb( "guid", HelpText = "generate new Guid(s)")]
        public class CommandOption : ICommandOption
        {
            [Value( 0, HelpText = "Number of Generations", Default = 1)]
            public int Number { get; set; } = 1;

            [Option( 'o', "output" )]
            public string OutputPath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;

            var number = option.Number;
            var outputPath = option.OutputPath;
            var outputText = new StringBuilder( 256 );
            var enableOutputText = !StringHelper.IsEmpty( outputPath );

            if( number <= 0 )
            {
                return 0;
            }

            for( var i = 0; i < number; i++ )
            {
                var guid = Guid.NewGuid().ToString( "D" );

                if( enableOutputText )
                {
                    outputText.Append( guid ).Append( Environment.NewLine );
                }
                else
                {
                    Console.WriteLine( guid );
                }
            }

            if( enableOutputText )
            {
                File.WriteAllText( outputPath, outputText.ToString() );
            }

            Console.WriteLine();
            Console.WriteLine( $"{number} Guid generated" );

            return 0;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using CommandLine;

namespace KeySwitchManager.CLI.Commands
{
    public class BatchMode : ICommand
    {
        [Verb( "batch", HelpText = "batch mode command")]
        [SuppressMessage( "ReSharper", "MemberCanBePrivate.Global" )]
        [SuppressMessage( "ReSharper", "ClassNeverInstantiated.Global" )]
        public class CommandOption : ICommandOption
        {
            [Option( 'f', "file", Required = true )]
            public string BatchFilePath { get; set; } = string.Empty;
        }

        public int Execute( ICommandOption opt )
        {
            var option = (CommandOption)opt;
            var batchText = File.ReadAllLines( option.BatchFilePath );

            var stopWatch = Stopwatch.StartNew();

            Console.WriteLine( "*** Batch mode begin" );

            foreach( var x in batchText )
            {
                var line = x.Trim();

                if( string.IsNullOrEmpty( line ) || line.StartsWith( "#" ) )
                {
                    continue;
                }

                var commandOption = SplitCommandLine( line );

                Console.WriteLine( $"*** Command Option: {line})" );

                var exitCode = Program.Execute( commandOption );

                if( exitCode != 0 )
                {
                    Console.WriteLine( $"Aborted. (Exit code={exitCode})" );
                    Console.WriteLine( $"Command Option: {line})" );
                    return 1;
                }

            }

            stopWatch.Stop();
            Console.WriteLine( $"{batchText.Length} batch processed in {stopWatch.ElapsedMilliseconds} ms." );

            return 0;
        }

        private static string[] SplitCommandLine( string line )
        {
            void AddToList( StringBuilder stringBuilder, List<string> list1 )
            {
                if( stringBuilder.Length > 0 )
                {
                    list1.Add( stringBuilder.ToString() );
                    stringBuilder.Clear();
                }
            }

            var builder = new StringBuilder();
            var list = new List<string>();

            var inDoubleQuote = false;

            foreach( var c in line )
            {
                switch( c )
                {
                    case '"' when inDoubleQuote:
                        AddToList( builder, list );
                        inDoubleQuote = false;
                        break;

                    case '"':
                        inDoubleQuote = true;
                        break;

                    case ' ' when inDoubleQuote:
                    case '\t' when inDoubleQuote:
                        builder.Append( c );
                        break;

                    case ' ':
                    case '\t':
                        AddToList( builder, list );
                        break;

                    default:
                        builder.Append( c );
                        break;
                }
            }

            AddToList( builder, list );
            return list.ToArray();
        }
    }
}
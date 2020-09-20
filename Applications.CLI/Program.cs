using System;

using CommandLine;

using KeySwitchManager.Apps.CLI.Commands;

namespace KeySwitchManager.Apps.CLI
{
    public class Program
    {
        public static int Main( string[] args ) =>
            Parser.Default.ParseArguments
                   <
                       Export.CommandOption,
                       Import.CommandOption
                   >( args )
                  .MapResult(
                       ( Export.CommandOption option ) => new Export().Execute( option ),
                       ( Import.CommandOption option ) => new Import().Execute( option ),
                       errors => 1
                   );
    }
}
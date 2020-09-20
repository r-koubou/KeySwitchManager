using System;

using CommandLine;

using KeySwitchManager.Apps.CLI.Commands;

namespace KeySwitchManager.Apps.CLI
{
    public class Program
    {
        public static int Main( string[] args )
        {
            return Parser.Default.ParseArguments<
                       Export.CommandOption
                   >( args )
                  .MapResult(
                       ( Export.CommandOption option ) => new Export().Execute( option ),
                       errors => 1
                   );
        }

        private static int RunAddAndReturnExitCode( ICommandOption opts )
        {
            throw new NotImplementedException();
        }
    }
}
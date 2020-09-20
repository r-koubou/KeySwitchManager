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
                       Import.CommandOption,
                       Delete.CommandOption,
                       Template.CommandOption,
                       ExportVstExpressionMap.CommandOption
                   >( args )
                  .MapResult(
                       ( Export.CommandOption option ) => new Export().Execute( option ),
                       ( Import.CommandOption option ) => new Import().Execute( option ),
                       ( Delete.CommandOption option ) => new Delete().Execute( option ),
                       ( Template.CommandOption option ) => new Template().Execute( option ),
                       ( ExportVstExpressionMap.CommandOption option ) => new ExportVstExpressionMap().Execute( option ),
                       errors => 1
                   );
    }
}
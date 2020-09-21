using CommandLine;

using KeySwitchManager.Apps.CLI.Commands;

namespace KeySwitchManager.Apps.CLI
{
    public class Program
    {
        public static int Main( string[] args ) =>
            Parser.Default.ParseArguments
                   <
                       Search.CommandOption,
                       Export.CommandOption,
                       Import.CommandOption,
                       Delete.CommandOption,
                       Template.CommandOption,
                       ExportVstExpressionMap.CommandOption,
                       ImportXlsx.CommandOption
                   >( args )
                  .MapResult(
                       ( Search.CommandOption option ) => new Search().Execute( option ),
                       ( Export.CommandOption option ) => new Export().Execute( option ),
                       ( Import.CommandOption option ) => new Import().Execute( option ),
                       ( Delete.CommandOption option ) => new Delete().Execute( option ),
                       ( Template.CommandOption option ) => new Template().Execute( option ),
                       ( ExportVstExpressionMap.CommandOption option ) => new ExportVstExpressionMap().Execute( option ),
                       ( ImportXlsx.CommandOption option ) => new ImportXlsx().Execute( option ),
                       errors => 1
                   );
    }
}
using CommandLine;

using KeySwitchManager.CLI.Commands;

namespace KeySwitchManager.CLI
{
    public class Program
    {
        public static int Main( string[] args ) =>
            Parser.Default.ParseArguments
                   <
                       Search.CommandOption,
                       Import.CommandOption,
                       Delete.CommandOption,
                       Template.CommandOption,
                       ImportXlsx.CommandOption,
                       ExportVstExpressionMap.CommandOption,
                       ExportStudioOneKeySwitch.CommandOption
                   >( args )
                  .MapResult(
                       ( Search.CommandOption option ) => new Search().Execute( option ),
                       ( Import.CommandOption option ) => new Import().Execute( option ),
                       ( Delete.CommandOption option ) => new Delete().Execute( option ),
                       ( Template.CommandOption option ) => new Template().Execute( option ),
                       ( ImportXlsx.CommandOption option ) => new ImportXlsx().Execute( option ),
                       ( ExportVstExpressionMap.CommandOption option ) => new ExportVstExpressionMap().Execute( option ),
                       ( ExportStudioOneKeySwitch.CommandOption option ) => new ExportStudioOneKeySwitch().Execute( option ),
                       errors => 1
                   );
    }
}
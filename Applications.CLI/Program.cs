using CommandLine;

using KeySwitchManager.CLI.Commands;

namespace KeySwitchManager.CLI
{
    public class Program
    {
        public static int Main( string[] args ) =>
            Execute( args );

        public static int Execute( string[] args ) =>
            Parser.Default.ParseArguments
                   <
                       BatchMode.CommandOption,
                       Search.CommandOption,
                       Import.CommandOption,
                       Delete.CommandOption,
                       Template.CommandOption,
                       ImportXlsx.CommandOption,
                       ExportVstExpressionMap.CommandOption,
                       ExportStudioOneKeySwitch.CommandOption
                   >( args )
                  .MapResult(
                       ( BatchMode.CommandOption option ) => new BatchMode().Execute( option ),
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
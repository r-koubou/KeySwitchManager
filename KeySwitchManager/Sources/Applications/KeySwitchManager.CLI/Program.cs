using CommandLine;

using KeySwitchManager.CLI.Commands;

namespace KeySwitchManager.CLI
{
    public class Program
    {
        public static int Main( string[] args ) =>
            Execute( args );

        public static int Execute( string[] args )
        {
            var types = new[]
            {
                typeof(BatchMode.CommandOption),
                typeof(Search.CommandOption),
                typeof(Import.CommandOption),
                typeof(Delete.CommandOption),
                typeof(Template.CommandOption),
                typeof(TemplateXlsx.CommandOption),
                typeof(ImportXlsx.CommandOption),
                typeof(ExportVstExpressionMap.CommandOption),
                typeof(ExportStudioOneKeySwitch.CommandOption),
                typeof(ExportCakewalkArticulation.CommandOption),
                typeof(ExportXlsx.CommandOption),
            };

            return Parser.Default.ParseArguments( args, types ).MapResult(
                ( BatchMode.CommandOption option ) => new BatchMode().Execute( option ),
                ( Search.CommandOption option ) => new Search().Execute( option ),
                ( Import.CommandOption option ) => new Import().Execute( option ),
                ( Delete.CommandOption option ) => new Delete().Execute( option ),
                ( Template.CommandOption option ) => new Template().Execute( option ),
                ( TemplateXlsx.CommandOption option ) => new TemplateXlsx().Execute( option ),
                ( ImportXlsx.CommandOption option ) => new ImportXlsx().Execute( option ),
                ( ExportVstExpressionMap.CommandOption option ) => new ExportVstExpressionMap().Execute( option ),
                ( ExportStudioOneKeySwitch.CommandOption option ) => new ExportStudioOneKeySwitch().Execute( option ),
                ( ExportCakewalkArticulation.CommandOption option ) => new ExportCakewalkArticulation().Execute( option ),
                ( ExportXlsx.CommandOption option ) => new ExportXlsx().Execute( option ),
                errors => 1
            );
        }
    }
}
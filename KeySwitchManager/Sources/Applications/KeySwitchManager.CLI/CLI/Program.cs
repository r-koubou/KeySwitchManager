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
                typeof(SearchKeySwitch.CommandOption),
                typeof(Import.CommandOption),
                typeof(Delete.CommandOption),
                typeof(Template.CommandOption),
                typeof(TemplateXlsx.CommandOption),
                typeof(ImportXlsx.CommandOption),
                typeof(ExportDawCubaseArticulation.CommandOption),
                typeof(ExportDawStudioOneArticulation.CommandOption),
                typeof(ExportDawCakewalkArticulation.CommandOption),
                typeof(ExportXlsx.CommandOption),
            };

            return Parser.Default.ParseArguments( args, types ).MapResult(
                ( BatchMode.CommandOption option ) => new BatchMode().Execute( option ),
                ( SearchKeySwitch.CommandOption option ) => new SearchKeySwitch().Execute( option ),
                ( Import.CommandOption option ) => new Import().Execute( option ),
                ( Delete.CommandOption option ) => new Delete().Execute( option ),
                ( Template.CommandOption option ) => new Template().Execute( option ),
                ( TemplateXlsx.CommandOption option ) => new TemplateXlsx().Execute( option ),
                ( ImportXlsx.CommandOption option ) => new ImportXlsx().Execute( option ),
                ( ExportDawCubaseArticulation.CommandOption option ) => new ExportDawCubaseArticulation().Execute( option ),
                ( ExportDawStudioOneArticulation.CommandOption option ) => new ExportDawStudioOneArticulation().Execute( option ),
                ( ExportDawCakewalkArticulation.CommandOption option ) => new ExportDawCakewalkArticulation().Execute( option ),
                ( ExportXlsx.CommandOption option ) => new ExportXlsx().Execute( option ),
                errors => 1
            );
        }
    }
}
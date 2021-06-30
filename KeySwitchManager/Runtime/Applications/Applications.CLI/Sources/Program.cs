using CommandLine;

using KeySwitchManager.Applications.CLI.Commands;

namespace KeySwitchManager.Applications.CLI
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
                typeof(New.CommandOption),
                typeof(NewXlsx.CommandOption),
                typeof(ImportXlsx.CommandOption),
                typeof(ExportDawCubaseArticulation.CommandOption),
                typeof(ExportDawStudioOneArticulation.CommandOption),
                typeof(ExportDawCakewalkArticulation.CommandOption),
                typeof(ExportXlsx.CommandOption),
                typeof(GenGuid.CommandOption),
            };

            return Parser.Default.ParseArguments( args, types ).MapResult(
                ( BatchMode.CommandOption option ) => new BatchMode().Execute( option ),
                ( Search.CommandOption option ) => new Search().Execute( option ),
                ( Import.CommandOption option ) => new Import().Execute( option ),
                ( Delete.CommandOption option ) => new Delete().Execute( option ),
                ( New.CommandOption option ) => new New().Execute( option ),
                ( NewXlsx.CommandOption option ) => new NewXlsx().Execute( option ),
                ( ImportXlsx.CommandOption option ) => new ImportXlsx().Execute( option ),
                ( ExportDawCubaseArticulation.CommandOption option ) => new ExportDawCubaseArticulation().Execute( option ),
                ( ExportDawStudioOneArticulation.CommandOption option ) => new ExportDawStudioOneArticulation().Execute( option ),
                ( ExportDawCakewalkArticulation.CommandOption option ) => new ExportDawCakewalkArticulation().Execute( option ),
                ( ExportXlsx.CommandOption option ) => new ExportXlsx().Execute( option ),
                ( GenGuid.CommandOption option ) => new GenGuid().Execute( option ),
                errors => 1
            );
        }
    }
}
using CommandLine;

using KeySwitchManager.Applications.Standalone.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawCakewalkArticulation : ExportDawArticulation
    {
        [Verb( "cakewalk", HelpText = "export to Cakewalk Articulation format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportFormat Format => ExportFormat.Cakewalk;
    }
}
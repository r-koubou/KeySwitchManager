using CommandLine;

using KeySwitchManager.Applications.Standalone.Core.Controllers.Export;
using KeySwitchManager.Controllers.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawCakewalkArticulation : ExportDawArticulation
    {
        [Verb( "cakewalk", HelpText = "export to Cakewalk Articulation format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportSupportedFormat SupportedFormat => ExportSupportedFormat.Cakewalk;
    }
}
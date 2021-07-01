using Application.Core.Controllers.Export;

using CommandLine;

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
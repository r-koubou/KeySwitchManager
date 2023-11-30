using CommandLine;

using KeySwitchManager.Controllers.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawLogicArticulation : ExportDawArticulation
    {
        [Verb( "logic", HelpText = "export to Logic format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportSupportedFormat SupportedFormat => ExportSupportedFormat.Logic;
    }
}

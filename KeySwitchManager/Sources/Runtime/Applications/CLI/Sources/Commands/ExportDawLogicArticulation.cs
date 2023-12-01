using CommandLine;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawLogicArticulation : ExportDawArticulation
    {
        [Verb( "logic", HelpText = "export to Logic format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportFormat Format => ExportFormat.Logic;
    }
}

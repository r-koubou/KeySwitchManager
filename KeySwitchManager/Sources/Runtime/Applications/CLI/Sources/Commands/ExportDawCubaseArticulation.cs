using CommandLine;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawCubaseArticulation : ExportDawArticulation
    {
        [Verb( "cubase", HelpText = "export to VST Expression map format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportFormat Format => ExportFormat.Cubase;
    }
}
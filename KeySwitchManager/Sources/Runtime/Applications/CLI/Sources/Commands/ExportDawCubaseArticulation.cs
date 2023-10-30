using CommandLine;

using KeySwitchManager.Applications.Core.Controllers.Export;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawCubaseArticulation : ExportDawArticulation
    {
        [Verb( "cubase", HelpText = "export to VST Expression map format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportSupportedFormat SupportedFormat => ExportSupportedFormat.Cubase;
    }
}
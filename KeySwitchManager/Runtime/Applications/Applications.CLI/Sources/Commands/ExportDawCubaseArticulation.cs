using Application.Core.Controllers.Export;

using CommandLine;

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
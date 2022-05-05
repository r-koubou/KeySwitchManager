using CommandLine;
using KeySwitchManager.Applications.Core.Controllers.Export;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportYaml : ExportDawArticulation
    {
        [Verb( "yaml", HelpText = "export a yaml format from database")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportSupportedFormat SupportedFormat => ExportSupportedFormat.Yaml;
    }
}
using CommandLine;

using KeySwitchManager.Applications.Standalone.Core.KeySwitches.Commons;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportYaml : ExportDawArticulation
    {
        [Verb( "yaml", HelpText = "export a yaml format from database")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportFormat Format => ExportFormat.Yaml;
    }
}
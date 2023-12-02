using CommandLine;

using KeySwitchManager.Applications.Standalone.Base.KeySwitches;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawStudioOneArticulation : ExportDawArticulation
    {
        [Verb( "studio-one", HelpText = "export to Studio One 5 Sound Variations format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        protected override ExportFormat Format => ExportFormat.StudioOne;
    }
}
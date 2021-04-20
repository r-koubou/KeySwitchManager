using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.StudioOne;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportDawStudioOneArticulation : ExportDawArticulation
    {
        [Verb( "studio-one", HelpText = "export to Studio One Articulation format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        public override IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory )
            => new StudioOneFileRepository( outputDirectory );
    }
}
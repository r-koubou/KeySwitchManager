using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportDawStudioOneArticulation : ExportDawArticulation
    {
        [Verb( "studio-one", HelpText = "export to Studio One 5 Sound Variations format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        public override IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory )
            => new StudioOneFileRepository( outputDirectory );
    }
}
using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.Cubase;

namespace KeySwitchManager.CLI.Commands
{
    public class ExportDawCubaseArticulation : ExportDawArticulation
    {
        [Verb( "cubase", HelpText = "export to Studio One Articulation format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        public override IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory )
            => new CubaseFileRepository( outputDirectory );
    }
}
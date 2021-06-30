using CommandLine;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.Cubase;

namespace KeySwitchManager.Applications.CLI.Commands
{
    public class ExportDawCubaseArticulation : ExportDawArticulation
    {
        [Verb( "cubase", HelpText = "export to VST Expression map format")]
        public new class CommandOption : ExportDawArticulation.CommandOption
        {}

        public override IKeySwitchRepository CreateOutputRepository( DirectoryPath outputDirectory )
            => new CubaseFileRepository( outputDirectory );
    }
}
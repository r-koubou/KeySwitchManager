using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public sealed class StudioOneGroupedExportPathBuilder : GroupedExportPathBuilder
    {
        public StudioOneGroupedExportPathBuilder() : this( DirectoryPath.Default ) {}

        public StudioOneGroupedExportPathBuilder( IDirectoryPath outputDirectory ) : base( ".keyswitch", outputDirectory ) {}
    }
}

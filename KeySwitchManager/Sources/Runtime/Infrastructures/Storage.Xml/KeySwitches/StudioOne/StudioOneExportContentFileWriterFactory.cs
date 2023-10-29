using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public sealed class StudioOneExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public StudioOneExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( outputDirectory, new DefaultExportPathBuilder( ".keyswitch", outputDirectory ) ) {}

        public StudioOneExportContentFileWriterFactory( DirectoryPath outputDirectory, IExportPathBuilder pathBuilder )
            : base( outputDirectory, pathBuilder ) {}
    }
}

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne
{
    public sealed class StudioOneExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public StudioOneExportContentFileWriterFactory( IDirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".keyswitch", outputDirectory ) ) {}

        public StudioOneExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

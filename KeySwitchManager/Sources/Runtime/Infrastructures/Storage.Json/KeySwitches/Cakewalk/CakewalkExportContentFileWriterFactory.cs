using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk
{
    public sealed class CakewalkExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public CakewalkExportContentFileWriterFactory() : base( ".artmap" ) {}

        public CakewalkExportContentFileWriterFactory( IDirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".artmap", outputDirectory ) ) {}

        public CakewalkExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

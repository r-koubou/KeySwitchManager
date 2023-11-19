using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase
{
    public sealed class CubaseExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public CubaseExportContentFileWriterFactory() : base( ".expressionmap" ) {}

        public CubaseExportContentFileWriterFactory( IDirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".expressionmap", outputDirectory ) ) {}

        public CubaseExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

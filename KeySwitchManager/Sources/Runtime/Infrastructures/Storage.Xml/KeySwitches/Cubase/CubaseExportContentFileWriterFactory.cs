using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase
{
    public sealed class CubaseExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public CubaseExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( outputDirectory, new DefaultExportPathBuilder( ".xml", outputDirectory ) ) {}

        public CubaseExportContentFileWriterFactory( DirectoryPath outputDirectory, IExportPathBuilder pathBuilder )
            : base( outputDirectory, pathBuilder ) {}
    }
}

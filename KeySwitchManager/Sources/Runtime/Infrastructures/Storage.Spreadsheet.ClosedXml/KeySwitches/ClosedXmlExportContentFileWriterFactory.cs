using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public sealed class ClosedXmlExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public ClosedXmlExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".xlsx", outputDirectory ) ) {}

        public ClosedXmlExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

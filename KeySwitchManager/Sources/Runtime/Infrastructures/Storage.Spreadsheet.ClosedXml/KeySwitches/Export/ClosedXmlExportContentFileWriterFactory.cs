using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Export
{
    public sealed class ClosedXmlExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public ClosedXmlExportContentFileWriterFactory()
            : this( new DefaultExportPathBuilder( ".xlsx" ) ) {}

        public ClosedXmlExportContentFileWriterFactory( IDirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".xlsx", outputDirectory ) ) {}

        public ClosedXmlExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

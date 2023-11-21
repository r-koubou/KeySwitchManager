using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic
{
    public sealed class LogicExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public LogicExportContentFileWriterFactory() : base( ".plist" ) {}

        public LogicExportContentFileWriterFactory( IDirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".plist", outputDirectory ) ) {}

        public LogicExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

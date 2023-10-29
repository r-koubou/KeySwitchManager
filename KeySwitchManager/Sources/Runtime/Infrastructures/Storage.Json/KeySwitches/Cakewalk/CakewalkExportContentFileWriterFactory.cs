using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk
{
    public sealed class CakewalkExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public CakewalkExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( outputDirectory, new DefaultExportPathBuilder( ".json", outputDirectory ) ) {}

        public CakewalkExportContentFileWriterFactory( DirectoryPath outputDirectory, IExportPathBuilder pathBuilder )
            : base( outputDirectory, pathBuilder ) {}
    }
}

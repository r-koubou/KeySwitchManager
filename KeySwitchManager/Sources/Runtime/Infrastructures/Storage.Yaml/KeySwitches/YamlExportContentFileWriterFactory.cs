using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public YamlExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( new DefaultExportPathBuilder( ".yaml", outputDirectory ) ) {}

        public YamlExportContentFileWriterFactory( IExportPathBuilder pathBuilder )
            : base( pathBuilder ) {}
    }
}

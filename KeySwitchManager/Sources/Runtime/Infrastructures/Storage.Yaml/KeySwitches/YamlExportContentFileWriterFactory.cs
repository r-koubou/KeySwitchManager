using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches
{
    public sealed class YamlExportContentFileWriterFactory : KeySwitchExportContentFileWriterFactory
    {
        public YamlExportContentFileWriterFactory( DirectoryPath outputDirectory )
            : base( outputDirectory, new DefaultExportPathBuilder( ".yaml", outputDirectory ) ) {}

        public YamlExportContentFileWriterFactory( DirectoryPath outputDirectory, IExportPathBuilder pathBuilder )
            : base( outputDirectory, pathBuilder ) {}
    }
}

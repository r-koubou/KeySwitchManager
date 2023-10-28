using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class KeySwitchExportContentFileWriterFactory : IExportContentWriterFactory<KeySwitch>
    {
        private string Suffix { get; }
        private DirectoryPath OutputDirectory { get; }

        public KeySwitchExportContentFileWriterFactory( string suffix, DirectoryPath outputDirectory )
        {
            Suffix          = suffix;
            OutputDirectory = outputDirectory;
        }

        public IExportContentWriter Create( KeySwitch source )
        {
            var outputPath = CreatePathHelper.CreateFilePath( source, Suffix, OutputDirectory );
            OutputDirectory.CreateNew();
            return new FileExportContentWriter( outputPath );
        }
    }
}

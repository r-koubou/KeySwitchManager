using System.IO;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class FileExportContentWriter : StreamExportContentWriter
    {
        private IFilePath OutputPath { get; }

        public FileExportContentWriter( IFilePath outputPath )
        {
            OutputPath = outputPath;
        }

        protected override Stream OpenStream()
        {
            return OutputPath.OpenWriteStream();
        }
    }
}

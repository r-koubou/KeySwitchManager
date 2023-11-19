using System;
using System.IO;

using KeySwitchManager.Commons.Data;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class FileExportContentWriter : AbstractStreamExportContentWriter
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

        protected override void DisposeStream( Stream stream )
        {
            Disposer.Dispose( stream );
        }
    }
}

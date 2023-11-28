using System.IO;
using System.IO.Compression;

using KeySwitchManager.Commons.Data;

using RkHelper.System;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public class ZipExportContentWriter : AbstractStreamExportContentWriter
    {
        private ZipArchive ZipArchive { get; }
        private IFilePath ArchivePath { get; }
        private CompressionLevel CompressionLevel { get; }

        public ZipExportContentWriter( ZipArchive zipArchive, IFilePath archivePath, CompressionLevel compressionLevel = CompressionLevel.Optimal )
        {
            ZipArchive       = zipArchive;
            ArchivePath      = archivePath;
            CompressionLevel = compressionLevel;
        }

        protected override Stream OpenStream()
        {
            return ZipArchive.CreateEntry( ArchivePath.Path, CompressionLevel ).Open();
        }

        protected override void DisposeStream( Stream stream )
        {
            Disposer.Dispose( stream );
        }

    }
}

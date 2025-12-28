using System.Collections.Generic;
using System.IO.Compression;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public sealed class ZipExportContentFileWriterFactory : IExportContentWriterFactory
    {
        private IExportPathBuilder ZipEntryPathBuilder { get; }
        private ZipArchive Archive { get; }

        public ZipExportContentFileWriterFactory( ZipArchive zipArchive, string exportFileSuffix )
          : this( zipArchive, new DefaultExportPathBuilder( exportFileSuffix ) ) {}

        public ZipExportContentFileWriterFactory( ZipArchive zipArchive, IExportPathBuilder zipEntryPathBuilder )
        {
            Archive             = zipArchive;
            ZipEntryPathBuilder = zipEntryPathBuilder;
        }

        public Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            var outputPath = ZipEntryPathBuilder.Build( keySwitches );
            return Task.FromResult<IExportContentWriter>( new ZipExportContentWriter( Archive, outputPath ) );
        }
    }
}

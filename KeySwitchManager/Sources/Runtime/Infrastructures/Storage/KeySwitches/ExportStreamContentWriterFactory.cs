using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class ExportStreamContentWriterFactory : IExportContentWriterFactory
    {
        protected Stream TargetStream { get; }

        protected ExportStreamContentWriterFactory( Stream targetStream )
        {
            TargetStream = targetStream;
        }

        public Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default )
        {
            return Task.FromResult<IExportContentWriter>( CreateStreamExportContentWriterImpl() );
        }

        protected abstract AbstractStreamExportContentWriter CreateStreamExportContentWriterImpl();
    }
}

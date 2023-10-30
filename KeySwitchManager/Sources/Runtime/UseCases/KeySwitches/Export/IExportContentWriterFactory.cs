using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportContentWriterFactory
    {
        IExportContentWriter Create( IReadOnlyCollection<KeySwitch> keySwitches )
            => CreateAsync( keySwitches, default ).GetAwaiter().GetResult();

        Task<IExportContentWriter> CreateAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken );
    }
}

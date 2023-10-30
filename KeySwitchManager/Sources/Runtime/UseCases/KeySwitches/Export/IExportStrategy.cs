using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportStrategy
    {
        Task ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, CancellationToken cancellationToken = default );
    }
}

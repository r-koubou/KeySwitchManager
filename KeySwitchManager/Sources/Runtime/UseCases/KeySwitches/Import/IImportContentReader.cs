using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportContentReader
    {
        Task<IReadOnlyCollection<KeySwitch>> ReadAsync( IContent content, CancellationToken cancellationToken = default );
    }
}

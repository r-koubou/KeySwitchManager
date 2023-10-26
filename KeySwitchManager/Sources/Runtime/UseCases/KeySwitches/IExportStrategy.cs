using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public interface IExportStrategy
    {
        Task ExportAsync( IReadOnlyCollection<KeySwitch> keySwitches, IContentWriter contentWriter );
    }
}

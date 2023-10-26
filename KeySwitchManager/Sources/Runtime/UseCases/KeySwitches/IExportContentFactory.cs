using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches
{
    public interface IExportContentFactory
    {
        IContent Create( IReadOnlyCollection<KeySwitch> keySwitches );
    }
}

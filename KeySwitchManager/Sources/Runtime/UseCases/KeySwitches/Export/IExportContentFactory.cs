using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportContentFactory
    {
        IContent Create( IReadOnlyCollection<KeySwitch> keySwitches );
    }
}

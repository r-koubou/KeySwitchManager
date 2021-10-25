using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IPackedKeySwitchReader : IKeySwitchReader
    {
        public IReadOnlyCollection<KeySwitch> ReadAll()
            => throw new System.NotImplementedException();
    }
}

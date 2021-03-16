using System;
using System.Collections.Generic;

namespace KeySwitchManager.Gateways.KeySwitch
{
    public interface IKeySwitchSpreadSheetRepository : IDisposable
    {
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Load();
        public bool Save( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> keySwitches );
    }
}
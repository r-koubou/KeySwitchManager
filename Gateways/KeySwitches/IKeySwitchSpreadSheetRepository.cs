using System;
using System.Collections.Generic;
using System.IO;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.KeySwitches.Aggregate;

namespace KeySwitchManager.Gateways.KeySwitches
{
    public interface IKeySwitchSpreadSheetRepository : IDisposable
    {
        public IReadOnlyCollection<KeySwitch> Load();
        public bool Save( IReadOnlyCollection<KeySwitch> keySwitch );
    }
}
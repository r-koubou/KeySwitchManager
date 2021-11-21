using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    [Obsolete]
    public class ExportDawResponse
    {
        public bool Result { get; }
        public IReadOnlyCollection<KeySwitch> StoredKeySwitches { get; }

        public ExportDawResponse(
            bool result,
            IEnumerable<KeySwitch> storedKeySwitches )
        {
            Result            = result;
            StoredKeySwitches = new List<KeySwitch>( storedKeySwitches );
        }
    }
}
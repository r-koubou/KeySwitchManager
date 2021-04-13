using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public class DawExportResponse
    {
        public bool Result { get; }
        public IReadOnlyCollection<KeySwitch> StoredKeySwitches { get; }

        public DawExportResponse(
            bool result,
            IEnumerable<KeySwitch> storedKeySwitches )
        {
            Result            = result;
            StoredKeySwitches = new List<KeySwitch>( storedKeySwitches );
        }
    }
}
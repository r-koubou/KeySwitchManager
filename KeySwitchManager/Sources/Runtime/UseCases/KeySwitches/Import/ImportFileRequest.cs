using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public class ImportFileRequest
    {
        public IReadOnlyCollection<KeySwitch> KeySwitches { get; }

        public ImportFileRequest( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            KeySwitches = new List<KeySwitch>( keySwitches );
        }
    }
}

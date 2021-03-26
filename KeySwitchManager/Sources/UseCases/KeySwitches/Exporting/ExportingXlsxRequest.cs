using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.UseCases.KeySwitches.Exporting
{
    public class ExportingXlsxRequest
    {
        public IReadOnlyCollection<KeySwitch> KeySwitches { get; }

        public ExportingXlsxRequest( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            KeySwitches = new List<KeySwitch>( keySwitches );
        }
    }
}
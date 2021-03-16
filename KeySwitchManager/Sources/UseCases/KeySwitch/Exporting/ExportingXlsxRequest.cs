using System.Collections.Generic;

namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public class ExportingXlsxRequest
    {
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> KeySwitches { get; }

        public ExportingXlsxRequest( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> keySwitches )
        {
            KeySwitches = new List<Domain.KeySwitches.KeySwitch>( keySwitches );
        }
    }
}
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public class SpreadsheetExportRequest
    {
        public IReadOnlyCollection<KeySwitch> KeySwitches { get; }

        public SpreadsheetExportRequest( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            KeySwitches = new List<KeySwitch>( keySwitches );
        }
    }
}
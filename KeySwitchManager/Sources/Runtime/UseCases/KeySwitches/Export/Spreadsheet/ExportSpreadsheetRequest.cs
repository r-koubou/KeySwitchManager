using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public class ExportSpreadsheetRequest
    {
        public IReadOnlyCollection<KeySwitch> KeySwitches { get; }

        public ExportSpreadsheetRequest( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            KeySwitches = new List<KeySwitch>( keySwitches );
        }
    }
}
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public class SpreadsheetImportResponse
    {
        public IReadOnlyCollection<KeySwitch> Imported { get; }
        public int Count => Imported.Count;

        public SpreadsheetImportResponse( IReadOnlyCollection<KeySwitch> imported )
        {
            Imported = imported;
        }
    }
}
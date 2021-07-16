using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public class ImportSpreadSheetResponse
    {
        public IReadOnlyCollection<KeySwitch> Imported { get; }
        public int Count => Imported.Count;

        public ImportSpreadSheetResponse( IReadOnlyCollection<KeySwitch> imported )
        {
            Imported = imported;
        }
    }
}
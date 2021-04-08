using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches;

namespace KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxResponse
    {
        public IReadOnlyCollection<KeySwitch> Imported { get; }
        public int Count => Imported.Count;

        public ImportingXlsxResponse( IReadOnlyCollection<KeySwitch> imported )
        {
            Imported = imported;
        }
    }
}
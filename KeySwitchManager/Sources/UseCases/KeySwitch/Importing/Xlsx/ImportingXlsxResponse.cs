using System.Collections.Generic;

namespace KeySwitchManager.UseCases.KeySwitch.Importing.Xlsx
{
    public class ImportingXlsxResponse
    {
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Imported { get; }
        public int Count => Imported.Count;

        public ImportingXlsxResponse( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> imported )
        {
            Imported = imported;
        }
    }
}
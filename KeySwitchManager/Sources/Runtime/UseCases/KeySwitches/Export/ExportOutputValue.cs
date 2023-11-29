using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class ExportOutputValue
    {
        public ExportInputValue Input { get; }
        public IReadOnlyCollection<KeySwitch> Result { get; }
        public int WrittenCount { get; }

        public ExportOutputValue( ExportInputValue input, IReadOnlyCollection<KeySwitch> result )
        {
            Input        = input;
            Result       = result;
            WrittenCount = result.Count;
        }
    }
}

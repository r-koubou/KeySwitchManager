using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.Commons;

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

    public class ExportOutputData : OutputData<ExportOutputValue>
    {
        public ExportOutputData( bool result, ExportOutputValue value, Exception? error = null ) : base( result, value, error ) {}
    }
}

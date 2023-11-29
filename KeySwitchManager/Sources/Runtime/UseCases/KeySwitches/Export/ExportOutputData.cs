using System;

using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class ExportOutputData : OutputData<ExportOutputValue>
    {
        public ExportOutputData( bool result, ExportOutputValue value, Exception? error = null ) : base( result, value, error ) {}
    }
}

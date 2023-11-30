using System;

using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public sealed class ImportOutputData : OutputData<ImportOutputValue>
    {
        public ImportOutputData( bool result, ImportOutputValue value, Exception? error = null ) : base( result, value, error ) {}
    }
}

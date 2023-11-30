using System;

using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Dump
{
    public sealed class DumpOutputData : OutputData<DumpOutputValue>
    {
        public DumpOutputData( bool result, DumpOutputValue value, Exception? error = null ) : base( result, value, error ) {}
    }
}

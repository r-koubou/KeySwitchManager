using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public sealed class CreateOutputData : IOutputData<KeySwitch>
    {
        public bool Result { get; }
        public KeySwitch Value { get; }
        public Exception? Error { get; }

        public CreateOutputData( bool result, KeySwitch response, Exception? error )
        {
            Result   = result;
            Value = response;
            Error    = error;
        }
    }
}

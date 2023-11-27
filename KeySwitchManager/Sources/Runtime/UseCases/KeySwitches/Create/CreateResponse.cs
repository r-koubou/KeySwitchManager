using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public sealed class CreateResponse : IResponse<KeySwitch>
    {
        public bool Result { get; }
        public KeySwitch Response { get; }
        public Exception? Error { get; }

        public CreateResponse( bool result, KeySwitch response, Exception? error )
        {
            Result   = result;
            Response = response;
            Error    = error;
        }
    }
}

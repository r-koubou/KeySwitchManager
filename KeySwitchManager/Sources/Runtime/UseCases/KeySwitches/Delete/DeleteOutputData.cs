using System;

using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public sealed class DeleteOutputData : IOutputData<DeleteOutputValue>
    {
        public bool Result { get; }
        public DeleteOutputValue Value { get; }
        public Exception? Error { get; }

        public DeleteOutputData( bool result, DeleteOutputValue value, Exception? error )
        {
            Result = result;
            Value  = value;
            Error  = error;
        }
    }
}

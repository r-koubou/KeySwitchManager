using System;
using System.Collections.Generic;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchWriter : IDisposable
    {
        public const int DefaultStreamWriterBufferSize = 1024; // System.IO.StreamWriter.DefaultBufferSize is 1024
        public bool LeaveOpen { get; }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null );
    }
}

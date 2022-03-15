using System;
using System.Collections.Generic;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchReader : IDisposable
    {
        public const int DefaultStreamReaderBufferSize = 4096; // System.IO.StreamReader.DefaultBufferSize is 4096
        public bool LeaveOpen { get; }
        public IReadOnlyCollection<KeySwitch> Read( IObserver<string>? loggingSubject = null );
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Domain.KeySwitches
{
    public interface IKeySwitchWriter : IDisposable
    {
        public const int DefaultStreamWriterBufferSize = 1024; // System.IO.StreamWriter.DefaultBufferSize is 1024
        public bool LeaveOpen { get; }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null )
            => WriteAsync( keySwitches, loggingSubject ).GetAwaiter().GetResult();

        public Task WriteAsync( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject );
    }
}

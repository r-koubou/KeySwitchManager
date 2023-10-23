using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchReader : IDisposable
    {
        public const int DefaultStreamReaderBufferSize = 4096; // System.IO.StreamReader.DefaultBufferSize is 4096
        public bool LeaveOpen { get; }

        public IReadOnlyCollection<KeySwitch> Read( IObserver<string>? loggingSubject = null )
            => ReadAsync( loggingSubject ).GetAwaiter().GetResult();

        public Task<IReadOnlyCollection<KeySwitch>> ReadAsync( IObserver<string>? loggingSubject );
    }
}

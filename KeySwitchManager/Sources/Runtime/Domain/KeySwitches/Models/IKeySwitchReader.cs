using System;
using System.Collections.Generic;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchReader : IDisposable
    {
        public bool LeaveOpen { get; }
        public IReadOnlyCollection<KeySwitch> Read( IObserver<string>? loggingSubject = null );
    }
}

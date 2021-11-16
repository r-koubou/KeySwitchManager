using System;
using System.Collections.Generic;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchWriter : IDisposable
    {
        public bool LeaveOpen { get; }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, IObserver<string>? loggingSubject = null );
    }
}

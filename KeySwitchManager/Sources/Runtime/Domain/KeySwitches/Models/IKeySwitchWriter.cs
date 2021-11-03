using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchWriter : IDisposable
    {
        public bool LeaveOpen { get; }

        public void Write( IReadOnlyCollection<KeySwitch> keySwitches, Subject<string>? loggingSubject = null );
    }
}

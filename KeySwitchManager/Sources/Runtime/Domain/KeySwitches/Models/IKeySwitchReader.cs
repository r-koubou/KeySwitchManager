using System;
using System.Collections.Generic;
using System.Reactive.Subjects;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchReader : IDisposable
    {
        public bool LeaveOpen { get; }
        public IReadOnlyCollection<KeySwitch> Read( Subject<string>? loggingSubject = null );
    }
}

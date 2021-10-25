using System;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchWriter : IDisposable
    {
        public void Write( KeySwitch keySwitch );
        public void Flush();
        public void Close();
    }
}

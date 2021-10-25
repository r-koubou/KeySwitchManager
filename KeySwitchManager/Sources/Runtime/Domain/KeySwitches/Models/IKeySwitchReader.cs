using System;

namespace KeySwitchManager.Domain.KeySwitches.Models
{
    public interface IKeySwitchReader : IDisposable
    {
        public KeySwitch Read();
        public void Close();
    }
}

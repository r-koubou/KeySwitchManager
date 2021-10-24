using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IKeySwitchReader
    {
        public KeySwitch Read();
    }
}

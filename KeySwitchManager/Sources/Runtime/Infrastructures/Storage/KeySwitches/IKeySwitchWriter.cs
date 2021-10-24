using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IKeySwitchWriter
    {
        public void Write( KeySwitch keySwitch );
    }
}

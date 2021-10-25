using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IKeySwitchFileReader : IKeySwitchReader
    {
        public FilePath Target { get; }
    }
}

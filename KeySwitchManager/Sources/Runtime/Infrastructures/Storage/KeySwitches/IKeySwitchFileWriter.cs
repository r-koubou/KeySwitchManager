using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IKeySwitchFileWriter : IKeySwitchWriter
    {
        public FilePath Target { get; }
    }
}

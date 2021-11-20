using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class KeySwitchFileRepositoryFactory : IKeySwitchRepositoryFactory
    {
        public FilePath DataPath { get; }

        protected KeySwitchFileRepositoryFactory( FilePath dataPath )
        {
            DataPath = dataPath;
        }

        public abstract IKeySwitchRepository Create();
    }
}

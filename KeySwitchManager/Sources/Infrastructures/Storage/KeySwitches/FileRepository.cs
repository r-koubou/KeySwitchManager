using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class FileRepository : OnMemoryKeySwitchRepository
    {
        public IPath DataPath { get; }

        protected FileRepository( IPath dataPath, bool loadFromPathNow )
        {
            DataPath = dataPath;

            if( DataPath.IsFile && loadFromPathNow )
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                Load();
            }
        }

        public abstract override int Flush();
        public abstract void Load();
    }
}
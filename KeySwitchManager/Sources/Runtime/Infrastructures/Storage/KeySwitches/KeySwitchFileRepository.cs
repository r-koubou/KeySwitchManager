using System;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class KeySwitchFileRepository : OnMemoryKeySwitchRepository
    {
        public IPath DataPath { get; }

        [Obsolete]
        protected KeySwitchFileRepository( IPath dataPath, bool loadFromPathNow )
        {
            DataPath = dataPath;

            if( DataPath.IsFile && loadFromPathNow )
            {
                // ReSharper disable once VirtualMemberCallInConstructor
                Load();
            }
        }

        [Obsolete]
        public abstract override int Flush();
        [Obsolete]
        public abstract void Load();
    }
}
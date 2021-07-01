using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class KeySwitchFileRepository : OnMemoryKeySwitchRepository
    {
        public IPath DataPath { get; }

        protected KeySwitchFileRepository( IPath dataPath, bool loadFromPathNow )
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
using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class LoadOnlyKeySwitchFileRepository : KeySwitchFileRepository
    {
        protected LoadOnlyKeySwitchFileRepository( IPath dataPath ) :
            base( dataPath, true ) {}

        protected LoadOnlyKeySwitchFileRepository( IPath dataPath, bool loadFromPathNow ) :
            base( dataPath, loadFromPathNow ) {}

        public override int Flush()
            => throw new NotSupportedException(
                $"class `{GetType().Name}` is not supported save"
            );
    }
}
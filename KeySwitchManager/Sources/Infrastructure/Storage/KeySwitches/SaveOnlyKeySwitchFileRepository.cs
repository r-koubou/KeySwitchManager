using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public abstract class SaveOnlyKeySwitchFileRepository : KeySwitchFileRepository
    {
        protected SaveOnlyKeySwitchFileRepository( IPath dataPath ) :
            base( dataPath, false ) {}

        public override void Load()
            => throw new NotSupportedException(
                $"class `{GetType().Name}` is not supported load"
            );
    }
}
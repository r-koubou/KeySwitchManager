using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public abstract class SaveOnlyKeySwitchFileRepository : KeySwitchFileRepository
    {
        protected IStorageAccessListener StorageAccessListener { get; }

        protected SaveOnlyKeySwitchFileRepository( IPath dataPath ) :
            this( dataPath, IStorageAccessListener.Null ) {}

        protected SaveOnlyKeySwitchFileRepository( IPath dataPath, IStorageAccessListener listener ) :
            base( dataPath, false )
        {
            StorageAccessListener = listener;
        }

        public override void Load()
            => throw new NotSupportedException(
                $"class `{GetType().Name}` is not supported load"
            );
    }
}
using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public abstract class LoadOnlyKeySwitchFileRepository : KeySwitchFileRepository
    {
        protected IStorageAccessListener StorageAccessListener { get; }

        protected LoadOnlyKeySwitchFileRepository( IPath dataPath ) :
            this( dataPath, IStorageAccessListener.Null ) {}

        protected LoadOnlyKeySwitchFileRepository( IPath dataPath, bool loadFromPathNow ) :
            this( dataPath, loadFromPathNow, IStorageAccessListener.Null ) {}

        protected LoadOnlyKeySwitchFileRepository( IPath dataPath, IStorageAccessListener listener ) :
            base( dataPath, true )
        {
            StorageAccessListener = listener;
        }

        protected LoadOnlyKeySwitchFileRepository( IPath dataPath, bool loadFromPathNow, IStorageAccessListener listener ) :
            base( dataPath, loadFromPathNow )
        {
            StorageAccessListener = listener;
        }

        public override int Flush()
            => throw new NotSupportedException(
                $"class `{GetType().Name}` is not supported save"
            );
    }
}
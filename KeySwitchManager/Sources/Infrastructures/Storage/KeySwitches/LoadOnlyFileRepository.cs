using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public abstract class LoadOnlyFileRepository : FileRepository
    {
        protected LoadOnlyFileRepository( IPath dataPath ) :
            base( dataPath, true ) {}

        protected LoadOnlyFileRepository( IPath dataPath, bool loadFromPathNow ) :
            base( dataPath, loadFromPathNow ) {}

        public override int Flush()
            => throw new NotSupportedException(
                $"class `{GetType().Name}` is not supported save"
            );
    }
}
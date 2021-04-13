using System;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public abstract class SaveOnlyFileRepository : FileRepository
    {
        protected SaveOnlyFileRepository( IPath dataPath ) :
            base( dataPath, false ) {}

        public override void Load()
            => throw new NotSupportedException(
                $"Sorry, `{nameof( SaveOnlyFileRepository )}` cannot load from file"
            );
    }
}
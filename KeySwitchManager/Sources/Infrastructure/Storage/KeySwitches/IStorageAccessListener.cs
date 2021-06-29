using System.Collections.Generic;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructure.Storage.KeySwitches
{
    public interface IStorageAccessListener
    {
        public static readonly IStorageAccessListener Null = new NoneImpl();

        void OnWriteAccess( IReadOnlyCollection<KeySwitch> keySwitches, IPath path ){}
        void OnReadAccess( IPath path ){}

        private class NoneImpl : IStorageAccessListener {}
    }
}

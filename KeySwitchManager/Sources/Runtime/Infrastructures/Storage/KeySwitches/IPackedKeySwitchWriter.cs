using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches
{
    public interface IPackedKeySwitchWriter : IKeySwitchWriter
    {
        public new void Write( KeySwitch keySwitch )
        {
            WriteAll( new List<KeySwitch> { keySwitch } );
        }

        public void WriteAll( IReadOnlyCollection<KeySwitch> keySwitches );
    }
}

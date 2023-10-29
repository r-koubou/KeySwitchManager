using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Domain.KeySwitches.Models;

namespace KeySwitchManager.Infrastructures.Storage.KeySwitches.Helper
{
    public static class KeySwitchValidateHelper
    {
        public static void ValidateOneElement( IReadOnlyCollection<KeySwitch> keySwitches )
        {
            if( !keySwitches.Any() )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} is empty" );
            }

            if( keySwitches.Count >= 2 )
            {
                throw new ArgumentException( $"{nameof( keySwitches )} has 1 element only" );
            }

        }

    }
}

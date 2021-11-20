using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Helpers
{
    public static class KeySwitchHelper
    {
        public static IReadOnlyDictionary<(DeveloperName,ProductName), List<KeySwitch>> GroupBy( IReadOnlyCollection<KeySwitch> source )
        {
            var productList = new Dictionary<(DeveloperName,ProductName), List<KeySwitch>>();

            foreach( var keySwitch in source )
            {
                var key = ( keySwitch.DeveloperName, keySwitch.ProductName );
                if( !productList.ContainsKey( key ) )
                {
                    productList[ key ] = new List<KeySwitch>();
                }
                productList[ key ].Add( keySwitch );
            }

            return productList;
        }

    }
}

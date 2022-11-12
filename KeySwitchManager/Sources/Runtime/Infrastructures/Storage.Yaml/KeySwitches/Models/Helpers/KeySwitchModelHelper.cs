using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Helpers
{
    public static class KeySwitchModelHelper
    {
        public static IReadOnlyCollection<KeySwitchModel> SortByAlphabetical( IReadOnlyCollection<KeySwitchModel> source )
            => source.OrderBy( x => x.DeveloperName )
                     .ThenBy( x => x.ProductName )
                     .ThenBy( x => x.InstrumentName )
                     .ToList();

        public static IReadOnlyDictionary<(string,string), List<KeySwitchModel>> GroupBy( IReadOnlyCollection<KeySwitchModel> source )
        {
            var productList = new Dictionary<(string,string), List<KeySwitchModel>>();

            foreach( var keySwitch in source )
            {
                var key = ( keySwitch.DeveloperName, keySwitch.ProductName );
                if( !productList.ContainsKey( key ) )
                {
                    productList[ key ] = new List<KeySwitchModel>();
                }
                productList[ key ].Add( keySwitch );
            }

            return productList;
        }
    }
}

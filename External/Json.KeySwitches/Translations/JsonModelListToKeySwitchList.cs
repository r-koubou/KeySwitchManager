using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Json.KeySwitches.Helpers;
using KeySwitchManager.Json.KeySwitches.Models;
using KeySwitchManager.UseCases.KeySwitches.Translations;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class JsonModelListToKeySwitchList : IJsonListToKeySwitchList
    {
        public IReadOnlyCollection<KeySwitch> Translate( IText source )
        {
            var result = new List<KeySwitch>();
            var model = JsonConvert.DeserializeObject<IEnumerable<KeySwitchModel>>( source.Value );

            foreach( var i in model )
            {
                var x = JsonModelToKeySwitchHelper.Translate( i );
                result.Add( x );
            }

            return result;
        }
    }
}
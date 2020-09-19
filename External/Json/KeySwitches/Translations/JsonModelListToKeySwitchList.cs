using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Json.KeySwitches.Model;
using KeySwitchManager.Json.KeySwitches.Services;
using KeySwitchManager.UseCases.KeySwitches.Translations;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class JsonModelListToKeySwitchList : IJsonListToKeySwitchList
    {
        public IEnumerable<KeySwitch> Translate( IText source )
        {
            var result = new List<KeySwitch>();
            var model = JsonConvert.DeserializeObject<IEnumerable<KeySwitchModel>>( source.Value );

            foreach( var i in model )
            {
                var x = JsonModelToKeySwitchService.Translate( i );
                result.Add( x );
            }

            return result;
        }
    }
}
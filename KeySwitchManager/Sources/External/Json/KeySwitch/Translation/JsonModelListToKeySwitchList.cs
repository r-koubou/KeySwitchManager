using System.Collections.Generic;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Json.KeySwitch.Helper;
using KeySwitchManager.Json.KeySwitch.Model;
using KeySwitchManager.UseCases.KeySwitch.Translations;

namespace KeySwitchManager.Json.KeySwitch.Translation
{
    public class JsonModelListToKeySwitchList : IJsonListToKeySwitchList
    {
        public IReadOnlyCollection<Domain.KeySwitches.KeySwitch> Translate( IText source )
        {
            var result = new List<Domain.KeySwitches.KeySwitch>();
            var model =JsonSerializer.Deserialize<IEnumerable<KeySwitchModel>>( source.Value ) ?? new List<KeySwitchModel>();

            foreach( var i in model )
            {
                var x = JsonModelToKeySwitchHelper.Translate( i );
                result.Add( x );
            }

            return result;
        }
    }
}
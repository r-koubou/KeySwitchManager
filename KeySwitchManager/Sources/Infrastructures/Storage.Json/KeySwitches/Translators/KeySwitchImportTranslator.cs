using System.Collections.Generic;
using System.Text.Json;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Translators.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Translators
{
    public class KeySwitchImportTranslator : IDataTranslator<IText, IReadOnlyCollection<KeySwitch>>
    {
        public IReadOnlyCollection<KeySwitch> Translate( IText source )
        {
            var result = new List<KeySwitch>();
            var model = JsonSerializer.Deserialize<IEnumerable<KeySwitchModel>>( source.Value ) ?? new List<KeySwitchModel>();

            foreach( var i in model )
            {
                var x = JsonModelToKeySwitchHelper.Translate( i );
                result.Add( x );
            }

            return result;
        }
    }
}
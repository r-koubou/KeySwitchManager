using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitch.Helper;
using KeySwitchManager.Json.KeySwitch.Model;

namespace KeySwitchManager.Json.KeySwitch.Translation
{
    public class JsonModelToKeySwitch : ITextToKeySwitch
    {
        public Domain.KeySwitches.KeySwitch Translate( IText source )
        {
            var model = JsonSerializer.Deserialize<KeySwitchModel>( source.Value ) ?? new KeySwitchModel();
            return JsonModelToKeySwitchHelper.Translate( model );
        }
    }
}
using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Models;
using KeySwitchManager.Json.KeySwitches.Services;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class JsonModelToKeySwitch : ITextToKeySwitch
    {
        public KeySwitch Translate( IText source )
        {
            var model = JsonConvert.DeserializeObject<KeySwitchModel>( source.Value );
            return JsonModelToKeySwitchService.Translate( model );
        }
    }
}
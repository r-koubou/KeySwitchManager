using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Helpers;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public bool Formatted { get; set; }

        public IText Translate( KeySwitch source )
        {
            var jsonRoot = KeySwitchToJsonModelHelper.Translate( source );
            var jsonText = JsonConvert.SerializeObject( jsonRoot, Formatted ? Formatting.Indented : Formatting.None );
            return new PlainText( jsonText );
        }
    }
}
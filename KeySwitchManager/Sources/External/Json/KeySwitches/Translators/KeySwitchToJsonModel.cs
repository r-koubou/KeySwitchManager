using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Helpers;

namespace KeySwitchManager.Json.KeySwitches.Translators
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public bool Formatted { get; set; }

        public IText Translate( KeySwitch source )
        {
            var jsonRoot = KeySwitchToJsonModelHelper.Translate( source );

            var serializeOption = new JsonSerializerOptions
            {
                Encoder       = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = Formatted
            };

            var jsonText = JsonSerializer.Serialize( jsonRoot, serializeOption );
            return new PlainText( jsonText );
        }
    }
}
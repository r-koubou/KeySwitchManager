using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitch.Helper;

namespace KeySwitchManager.Json.KeySwitch.Translation
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public bool Formatted { get; set; }

        public IText Translate( Domain.KeySwitches.KeySwitch source )
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
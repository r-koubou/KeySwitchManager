using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Translators.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Translators
{
    public class KeySwitchExportTranslator : IDataTranslator<KeySwitch, IText>
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
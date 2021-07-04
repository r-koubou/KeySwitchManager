using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Translators.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Json.Cakewalk.Translators
{
    public class CakewalkExportTranslator : IDataTranslator<KeySwitch, IText>
    {
        public bool Formatted { get; }

        public CakewalkExportTranslator( bool formatted = false )
        {
            Formatted = formatted;
        }

        public IText Translate( KeySwitch source )
        {
            var articulationMap = KeySwitchToCakewalkModelHelper.Translate( source );
            var serializeOption = new JsonSerializerOptions
            {
                Encoder       = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = Formatted
            };

            var jsonText = JsonSerializer.Serialize( articulationMap, serializeOption );

            return new PlainText( jsonText );
        }
    }
}
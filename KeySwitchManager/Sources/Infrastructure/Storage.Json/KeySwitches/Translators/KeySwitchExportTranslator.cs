using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Translators.Helpers;

namespace KeySwitchManager.Infrastructure.Storage.Json.KeySwitches.Translators
{
    public class KeySwitchExportTranslator :
        IDataTranslator<IReadOnlyCollection<KeySwitch>, IText>
    {
        public bool Formatted { get; }

        public KeySwitchExportTranslator( bool formatted = false )
        {
            Formatted = formatted;
        }

        public IText Translate( IReadOnlyCollection<KeySwitch> source )
        {
            var keySwitchList = new List<KeySwitchModel>();

            foreach( var i in source )
            {
                keySwitchList.Add( KeySwitchToJsonModelHelper.Translate( i ) );
            }

            var serializeOption = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                WriteIndented = Formatted
            };

            var jsonText = JsonSerializer.Serialize( keySwitchList, serializeOption );
            return new PlainText( jsonText );
        }
    }
}
using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Json.KeySwitches.Helpers;
using KeySwitchManager.Json.KeySwitches.Models;
using KeySwitchManager.UseCases.KeySwitch.Translations;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class KeySwitchListListToJsonModelList : IKeySwitchListToJsonListText
    {
        public bool Formatted { get; set; }

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
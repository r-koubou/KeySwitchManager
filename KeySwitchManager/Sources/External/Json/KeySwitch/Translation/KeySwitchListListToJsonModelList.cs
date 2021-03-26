using System.Collections.Generic;
using System.Text.Encodings.Web;
using System.Text.Json;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Json.KeySwitch.Helper;
using KeySwitchManager.Json.KeySwitch.Model;
using KeySwitchManager.UseCases.KeySwitches.Translators;

namespace KeySwitchManager.Json.KeySwitch.Translation
{
    public class KeySwitchListListToJsonModelList : IKeySwitchListToJsonListText
    {
        public bool Formatted { get; set; }

        public IText Translate( IReadOnlyCollection<Domain.KeySwitches.KeySwitch> source )
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
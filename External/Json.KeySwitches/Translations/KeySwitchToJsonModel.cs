using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.Translations;
using KeySwitchManager.Json.KeySwitches.Models;
using KeySwitchManager.Json.KeySwitches.Services;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class KeySwitchToJsonModel : IKeySwitchToText
    {
        public bool Formatted { get; set; }

        public IText Translate( KeySwitch source )
        {
            var jsonRoot = KeySwitchToJsonModelService.Translate( source );
            var jsonText = JsonConvert.SerializeObject( jsonRoot, Formatted ? Formatting.Indented : Formatting.None );
            return new PlainText( jsonText );
        }

        private static void ConvertMessageList(
            IEnumerable<IMidiMessage> src,
            ICollection<MidiMessageModel> dest )
        {
            foreach( var i in src )
            {
                dest.Add(
                    new MidiMessageModel(
                        i.Status.Value,
                        i.Channel.Value,
                        i.DataByte1.Value,
                        i.DataByte2.Value
                    )
                );
            }
        }

    }
}
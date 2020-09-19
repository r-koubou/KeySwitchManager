using System.Collections.Generic;

using KeySwitchManager.Domain.Commons;
using KeySwitchManager.Domain.KeySwitches.Aggregate;
using KeySwitchManager.Json.KeySwitches.Models;
using KeySwitchManager.Json.KeySwitches.Services;
using KeySwitchManager.UseCases.KeySwitches.Translations;

using Newtonsoft.Json;

namespace KeySwitchManager.Json.KeySwitches.Translations
{
    public class KeySwitchListListToJsonModelList : IKeySwitchListToJsonListText
    {
        public bool Formatted { get; set; }

        public IText Translate( IEnumerable<KeySwitch> source )
        {
            var keySwitchList = new List<KeySwitchModel>();

            foreach( var i in source )
            {
                keySwitchList.Add( KeySwitchToJsonModelService.Translate( i ) );
            }

            var jsonText = JsonConvert.SerializeObject( keySwitchList, Formatted ? Formatting.Indented : Formatting.None );
            return new PlainText( jsonText );
        }
    }
}
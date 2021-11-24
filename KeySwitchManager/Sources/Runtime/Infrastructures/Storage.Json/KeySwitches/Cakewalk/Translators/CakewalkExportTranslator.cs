using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Translators.Helpers;

using Newtonsoft.Json;

namespace KeySwitchManager.Infrastructures.Storage.Json.KeySwitches.Cakewalk.Translators
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
            var jsonRoot = KeySwitchToCakewalkModelHelper.Translate( source );
            var jsonText = JsonConvert.SerializeObject( jsonRoot, Formatted ? Formatting.Indented : Formatting.None );

            return new PlainText( jsonText );
        }
    }
}
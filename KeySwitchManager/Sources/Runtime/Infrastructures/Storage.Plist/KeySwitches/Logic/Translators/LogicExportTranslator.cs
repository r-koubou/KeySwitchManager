using Claunia.PropertyList;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Translators.Helpers;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Translators
{
    public class LogicExportTranslator : IDataTranslator<KeySwitch, NSDictionary>
    {
        public NSDictionary Translate( KeySwitch source )
            => TranslateModelHelper.Translate( source );
    }
}

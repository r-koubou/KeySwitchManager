using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators.Helpers;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Translators
{
    public class StudioOneExportTranslator : IDataTranslator<KeySwitch, IText>
    {
        public IText Translate( KeySwitch source )
        {
            return new PlainText(
                XmlHelper.ToXmlString( KeySwitchToStudioOneModelHelper.Translate( source ) )
            );
        }
    }
}
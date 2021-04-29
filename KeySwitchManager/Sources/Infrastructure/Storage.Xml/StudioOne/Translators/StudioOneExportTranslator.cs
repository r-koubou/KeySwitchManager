using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Translators.Helpers;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Translators
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
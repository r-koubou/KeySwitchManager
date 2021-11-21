using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators.Helpers;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.Cubase.Translators
{
    public class CubaseExportTranslator : IDataTranslator<KeySwitch, IText>
    {
        public IText Translate( KeySwitch source )
        {
            var rootElement = KeySwitchToCubaseModelHelper.Translate( source );
            return new PlainText( XmlHelper.ToXmlString( rootElement ) );
        }
    }
}
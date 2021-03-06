using KeySwitchManager.Commons.Data;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Translators.Helpers;

using RkHelper.Text.Xml;

namespace KeySwitchManager.Infrastructure.Storage.Xml.Cubase.Translators
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
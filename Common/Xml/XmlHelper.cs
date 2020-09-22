using System.Text;
using System.Xml;
using System.Xml.Serialization;

using KeySwitchManager.Common.IO;

namespace KeySwitchManager.Common.Xml
{
    public static class XmlHelper
    {
        public static string ToXmlString<T>( T rootElement, bool indent = true )
        {
            var serializer = new XmlSerializer( typeof( T ) );

            // no xmlns adding
            // see: https://stackoverflow.com/a/8882612
            var xmlNamespaces = new XmlSerializerNamespaces();
            xmlNamespaces.Add( "", "" );
            var stringWriter = new StringWriterWithEncoding( Encoding.UTF8 );
            var xmlWriterSettings = new XmlWriterSettings
            {
                Indent = indent
            };

            using var xmlWriter = XmlWriter.Create( stringWriter, xmlWriterSettings );
            serializer.Serialize( xmlWriter, rootElement, xmlNamespaces );

            return stringWriter.ToString();

        }
    }
}
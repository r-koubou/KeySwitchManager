using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Xml.Cubase.Models
{
    [XmlRoot( ElementName = "string" )]
    public class StringElement
    {
        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "value" )]
        public string Value { get; set; }  = string.Empty;

        [XmlAttribute( AttributeName = "wide" )]
        public bool Wide { get; set; } = true;

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public StringElement()
        {}

        public StringElement( string name, string value, bool wide = true )
        {
            Name  = name;
            Value = value;
            Wide  = wide;
        }
    }
}
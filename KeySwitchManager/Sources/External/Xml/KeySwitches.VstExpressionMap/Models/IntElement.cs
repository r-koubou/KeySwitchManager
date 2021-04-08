using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Xml.KeySwitches.VstExpressionMap.Models
{
    [XmlRoot( ElementName = "int" )]
    public class IntElement
    {
        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "value" )]
        public int Value { get; set; }

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public IntElement()
        {}

        public IntElement( string name, int value )
        {
            Name  = name;
            Value = value;
        }
    }
}
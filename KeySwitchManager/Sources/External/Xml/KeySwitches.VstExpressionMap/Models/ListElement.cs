using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Xml.KeySwitches.VstExpressionMap.Models
{
    [XmlRoot( ElementName = "list" )]
    public class ListElement
    {
        [XmlElement( ElementName = "obj" )]
        public List<ObjectElement> Obj { get; set; } = new List<ObjectElement>();

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "type" )]
        public string Type { get; set; }

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public ListElement()
        {
            Name = "obj";
            Type = "obj";
        }

        public ListElement( string name, string type )
        {
            Name = name;
            Type = type;
        }
    }
}
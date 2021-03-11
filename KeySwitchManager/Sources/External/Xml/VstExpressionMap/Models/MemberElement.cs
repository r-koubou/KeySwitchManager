using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Xml.VstExpressionMap.Models
{
    [XmlRoot( ElementName = "member" )]
    public class MemberElement
    {
        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlElement( ElementName = "int" )]
        public List<IntElement> Int { get; set; } = new List<IntElement>();

        [XmlElement( ElementName = "float" )]
        public List<FloatElement> Float { get; set; } = new List<FloatElement>();

        [XmlElement( ElementName = "string" )]
        public List<StringElement> String { get; set; } = new List<StringElement>();

        [XmlElement( ElementName = "obj" )]
        public List<ObjectElement> Obj { get; set; } = new List<ObjectElement>();

        [XmlElement( ElementName = "list" )]
        public List<ListElement> List { get; set; } = new List<ListElement>();

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public MemberElement()
        {}

        public MemberElement( string name )
        {
            Name = name;
        }
    }
}
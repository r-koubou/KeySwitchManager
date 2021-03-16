using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Xml.Serialization;

namespace KeySwitchManager.Xml.KeySwitch.VstExpressionMap.Model
{
    [XmlRoot( ElementName = "obj" )]
    public class ObjectElement
    {
        [XmlElement( ElementName = "int" )]
        public List<IntElement> Int { get; set; } = new List<IntElement>();

        [XmlElement( ElementName = "float" )]
        public List<FloatElement> Float { get; set; } = new List<FloatElement>();

        [XmlElement( ElementName = "string" )]
        public List<StringElement> String { get; set; } = new List<StringElement>();

        [XmlAttribute( AttributeName = "class" )]
        public string ClassName { get; set; }

        [XmlAttribute( AttributeName = "name" )]
        [MaybeNull]
        public string Name { get; set; } = default!;

        [XmlAttribute( AttributeName = "ID" )]
        public string Id { get; set; }

        [XmlElement( ElementName = "obj" )]
        public List<ObjectElement> Obj { get; set; } = new List<ObjectElement>();

        [XmlElement( ElementName = "member" )]
        public List<MemberElement> Member { get; set; } = new List<MemberElement>();

        [SuppressMessage( "ReSharper", "UnusedMember.Global" )]
        public ObjectElement() : this( string.Empty )
        {}

        public ObjectElement( string className )
        {
            var guid = Guid.NewGuid().ToString();
            var hex = guid.Split( '-' )[ 0 ];
            Id        = ( Convert.ToInt32( hex, 16 ) & 0x7FFFFFF ).ToString();
            ClassName = className ?? throw new ArgumentNullException( nameof( className ) );
        }
    }
}
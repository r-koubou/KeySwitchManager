using System.Collections.Generic;
using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models
{
    public class AttributeElement
    {
        [XmlAttribute( AttributeName = "folder" )]
        public string Folder { get; set; } = default!;

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "id" )]
        public string Id { get; set; } = default!;

        [XmlAttribute( AttributeName = "color" )]
        public string Color { get; set; } = default!; // AABBGGRR

        [XmlAttribute( AttributeName = "pitch" )]
        public string Pitch { get; set; } = default!;

        [XmlAttribute( AttributeName = "momentary" )]
        public string Momentary { get; set; } = default!;

        [XmlAttribute( AttributeName = "activation" ) ]
        public string Activation { get; set; } = default!;

        [XmlElement( ElementName = "Attributes" )]
        public List<AttributeElement> Children { get; } = new();

        public AttributeElement()
        {}

        public AttributeElement(
            string name,
            int id,
            string color,
            int pitch,
            int momentary,
            string activation )
        {
            Name       = name;
            Id         = id.ToString();
            Color      = color;
            Pitch      = pitch.ToString();
            Momentary  = momentary.ToString();
            Activation = activation;
        }
    }
}
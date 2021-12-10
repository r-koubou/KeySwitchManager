using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models
{
    public class ElementAttribute
    {
        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "id" )]
        public int Id { get; set; }

        [XmlAttribute( AttributeName = "color" )]
        public string Color { get; set; } = default!; // AABBGGRR

        [XmlAttribute( AttributeName = "pitch" )]
        public int Pitch { get; set; }

        [XmlAttribute( AttributeName = "momentary" )]
        public int Momentary { get; set; }

        [XmlAttribute( AttributeName = "activation" ) ]
        public string Activation { get; set; } = string.Empty;

        public ElementAttribute()
        {}

        public ElementAttribute(
            string name,
            int id,
            string color,
            int pitch,
            int momentary,
            string activation )
        {
            Name       = name;
            Id         = id;
            Color      = color;
            Pitch      = pitch;
            Momentary  = momentary;
            Activation = activation;
        }
    }
}
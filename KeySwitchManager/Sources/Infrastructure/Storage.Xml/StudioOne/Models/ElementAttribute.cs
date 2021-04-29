using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Models
{
    public class ElementAttribute
    {
        [XmlAttribute( AttributeName = "pitch" )]
        public string Pitch { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;
    }
}
using System.Collections.Generic;
using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructure.Storage.Xml.StudioOne.Models
{
    [XmlRoot( ElementName = "Music.KeySwitchList" )]
    public class RootElement
    {
        [XmlElement( ElementName = "Attributes" )]
        public List<ElementAttribute> Attributes { get; set; } = new List<ElementAttribute>();

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;
    }
}
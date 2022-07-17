using System.Collections.Generic;
using System.Xml.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Xml.KeySwitches.StudioOne.Models
{
    [XmlRoot( ElementName = "Music.KeySwitchList" )]
    public class RootElement
    {
        [XmlElement( ElementName = "Attributes" )]
        public List<AttributeElement> AttributeElements { get; set; } = new();

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;
    }
}
using System.Collections.Generic;
using System.Xml.Serialization;

namespace KeySwitchManager.Xml.KeySwitches.StudioOne.Models
{
    [XmlRoot( ElementName = "Music.KeySwitchList" )]
    public class RootElement
    {
        [XmlElement( ElementName = "Attributes" )]
        public List<Attributes> Attributes { get; set; } = new List<Attributes>();

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;
    }
}
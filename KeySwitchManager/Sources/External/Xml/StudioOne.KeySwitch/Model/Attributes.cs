using System.Xml.Serialization;

namespace KeySwitchManager.Xml.KeySwitch.StudioOne.Model
{
    public class Attributes
    {
        [XmlAttribute( AttributeName = "pitch" )]
        public string Pitch { get; set; } = string.Empty;

        [XmlAttribute( AttributeName = "name" )]
        public string Name { get; set; } = string.Empty;
    }
}
using YamlDotNet.Serialization;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class MidiControlChangeModel : IMidiChannelVoiceMessageModel
    {
        [YamlIgnore]
        public int Status => 0xB0 | ( Channel & 0xF );

        [YamlMember( Alias = "Channel" )]
        public int Channel { get; set; }

        [YamlMember( Alias = "ControlNumber" )]
        public int Data1 { get; set; }

        [YamlMember( Alias = "Data" )]
        public int Data2 { get; set; }

        public MidiControlChangeModel() {}

        public MidiControlChangeModel( int channel, int data1, int data2 )
        {
            Channel = channel;
            Data1   = data1;
            Data2   = data2;
        }
    }
}

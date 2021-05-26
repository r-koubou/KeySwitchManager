using YamlDotNet.Serialization;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities
{
    public class MidiProgramChangeModel : IMidiChannelVoiceMessageModel
    {
        [YamlIgnore]
        public int Status => 0xC0 | ( Channel & 0xF );

        [YamlMember( Alias = "Channel" )]
        public int Channel { get; set; }

        [YamlMember( Alias = "ProgramNumber" )]
        public int Data1 { get; set; }

        [YamlIgnore]
        public int Data2 { get; set; } = 0x00;

        public MidiProgramChangeModel() {}

        public MidiProgramChangeModel( int channel, int data )
        {
            Channel = channel;
            Data1   = data;
            Data2   = 0x00;
        }
    }
}

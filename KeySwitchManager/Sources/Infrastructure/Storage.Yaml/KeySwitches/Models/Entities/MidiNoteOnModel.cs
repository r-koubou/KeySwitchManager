using YamlDotNet.Serialization;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities
{
    public class MidiNoteOnModel : IMidiChannelVoiceMessageModel
    {
        [YamlIgnore]
        public int Status => 0x90 | ( Channel & 0xF );

        [YamlMember( Alias = "Channel" )]
        public int Channel { get; set; }

        [YamlMember( Alias = "NoteNumber" )]
        public int Data1 { get; set; }

        [YamlMember( Alias = "Velocity" )]
        public int Data2 { get; set; }

        public MidiNoteOnModel() {}

        public MidiNoteOnModel( int channel, int data1, int data2 )
        {
            Channel = channel;
            Data1   = data1;
            Data2   = data2;
        }
    }
}

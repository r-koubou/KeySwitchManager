using KeySwitchManager.Domain.MidiMessages.Models.Values;

using RkHelper.Primitives;

using YamlDotNet.Serialization;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public class MidiNoteOnModel : IMidiChannelVoiceMessageModel
    {
        [YamlIgnore]
        public int Status => 0x90 | ( Channel & 0xF );

        [YamlMember( Alias = "Channel" )]
        public int Channel { get; set; }

        [YamlMember( Alias = "Note" )]
        // ReSharper disable once MemberCanBePrivate.Global
        public string Note { get; set; } = string.Empty;

        [YamlIgnore]
        public int Data1
        {
            get
            {
                if( StringHelper.IsEmpty( Note ) )
                {
                    return 0;
                }

                if( NumberHelper.TryParse( Note, out var result ) ||
                    NumberHelper.TryParse( Note, out result, 16 ) )
                {
                    return result;
                }

                return new MidiNoteName( Note ).ToMidiNoteNumber().Value;
            }
            set => Note = MidiNoteName.FromMidiNoteNumber( new MidiNoteNumber( value ) ).Value;
        }

        [YamlMember( Alias = "Velocity" )]
        public int Data2 { get; set; }

        public MidiNoteOnModel() {}

        public MidiNoteOnModel( int channel, int data1, int data2 )
        {
            Channel = channel;
            Data1   = data1;
            Data2   = data2;
            Note    = MidiNoteName.FromMidiNoteNumber( new MidiNoteNumber( data1 ) ).Value;
        }
    }
}

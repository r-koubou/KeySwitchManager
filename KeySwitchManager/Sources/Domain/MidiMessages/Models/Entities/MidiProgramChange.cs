using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Entities
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class MidiProgramChange : IMidiChannelVoiceMessage
    {
        public IMidiMessageData Status => new MidiStatus( 0xC0 | Channel.Value );
        public IMidiMessageData Channel { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiProgramChange( MidiChannel channel, MidiProgramChangeNumber number )
        {
            Channel   = channel;
            DataByte1 = number;
            DataByte2 = new GenericMidiData( 0 );
        }
    }
}
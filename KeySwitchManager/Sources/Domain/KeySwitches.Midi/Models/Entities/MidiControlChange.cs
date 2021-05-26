using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class MidiControlChange : IMidiChannelVoiceMessage
    {
        public IMidiMessageData Status => new MidiStatus( 0xB0 | Channel.Value );
        public IMidiMessageData Channel { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiControlChange(
            MidiChannel channel,
            MidiControlChangeNumber midiControlChangeNumber,
            MidiControlChangeValue midiControlChangeValue )
        {
            Channel   = channel;
            DataByte1 = midiControlChangeNumber;
            DataByte2 = midiControlChangeValue;
        }
    }
}
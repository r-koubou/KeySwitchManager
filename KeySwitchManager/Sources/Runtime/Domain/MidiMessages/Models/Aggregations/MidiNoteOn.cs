using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Aggregations
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class MidiNoteOn : IMidiChannelVoiceMessage
    {
        public IMidiMessageData Status => new MidiStatus( 0x90 | Channel.Value );
        public IMidiMessageData Channel { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiNoteOn(
            MidiChannel channel,
            MidiNoteNumber midiNoteNumber,
            MidiVelocity midiVelocity )
        {
            Channel   = channel;
            DataByte1 = midiNoteNumber;
            DataByte2 = midiVelocity;
        }
    }
}
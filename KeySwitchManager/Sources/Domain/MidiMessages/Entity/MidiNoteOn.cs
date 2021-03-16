using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Entity
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class MidiNoteOn : IMidiMessage
    {
        public IMidiMessageData Status { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiNoteOn(
            MidiStatus status,
            MidiNoteNumber midiNoteNumber,
            MidiVelocity midiVelocity )
        {
            Status    = status;
            DataByte1 = midiNoteNumber;
            DataByte2 = midiVelocity;
        }
    }
}
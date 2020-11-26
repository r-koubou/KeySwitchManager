using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class MidiNoteOn : IMidiMessage
    {
        public IMidiMessageData Status { get; } = MidiStatusCode.NoteOn;
        public IMidiMessageData Channel { get; } = MidiChannel.Zero;
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiNoteOn( MidiNoteNumber midiNoteNumber, MidiVelocity midiVelocity )
        {
            DataByte1 = midiNoteNumber;
            DataByte2 = midiVelocity;
        }

        public MidiNoteOn( MidiChannel midiChannel, MidiNoteNumber midiNoteNumber, MidiVelocity midiVelocity )
        {
            Status    = MidiStatusCode.NoteOn;
            Channel   = midiChannel;
            DataByte1 = midiNoteNumber;
            DataByte2 = midiVelocity;
        }
    }
}
using System;

using ArticulationManager.Entities.MidiEvent.Value;

namespace ArticulationManager.Entities.MidiEvent.Aggregate
{
    /// <summary>
    /// Representing a MIDI note on.
    /// </summary>
    public class MidiNoteOn : IMidiEvent
    {
        public IMidiEventData Status { get; } = MidiStatusCode.NoteOn;
        public IMidiEventData DataByte1 { get; }
        public IMidiEventData DataByte2 { get; }

        public MidiNoteOn( MidiNoteNumber noteNumber, MidiVelocity velocity )
        {
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

        public MidiNoteOn( MidiChannel channel, MidiNoteNumber noteNumber, MidiVelocity velocity )
        {
            Status    = new MidiStatusCode( channel, MidiStatusCode.NoteOn.Value );
            DataByte1 = noteNumber;
            DataByte2 = velocity;
        }

    }
}
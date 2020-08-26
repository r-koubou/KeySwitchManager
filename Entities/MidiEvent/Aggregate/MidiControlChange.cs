using System;

using ArticulationManager.Entities.MidiEvent.Value;

namespace ArticulationManager.Entities.MidiEvent.Aggregate
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class MidiControlChange : IMidiEvent
    {
        public IMidiEventData Status { get; } = MidiStatusCode.ControlChange;
        public IMidiEventData DataByte1 { get; }
        public IMidiEventData DataByte2 { get; }

        public MidiControlChange( MidiControlChangeNumber controlChangeNumber, MidiControlChangeValue controlChangeValue )
        {
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }

        public MidiControlChange( MidiChannel channel, MidiControlChangeNumber controlChangeNumber, MidiControlChangeValue controlChangeValue )
        {
            Status    = new MidiStatusCode( channel, MidiStatusCode.ControlChange.Value );
            DataByte1 = controlChangeNumber;
            DataByte2 = controlChangeValue;
        }

    }
}
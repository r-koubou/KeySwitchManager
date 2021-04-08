using KeySwitchManager.Domain.MidiMessages.Values;

namespace KeySwitchManager.Domain.MidiMessages.Entity
{
    /// <summary>
    /// Represents a MIDI control change.
    /// </summary>
    public class MidiControlChange : IMidiMessage
    {
        public IMidiMessageData Status { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiControlChange(
            MidiStatus status,
            MidiControlChangeNumber midiControlChangeNumber,
            MidiControlChangeValue midiControlChangeValue )
        {
            Status    = status;
            DataByte1 = midiControlChangeNumber;
            DataByte2 = midiControlChangeValue;
        }
    }
}
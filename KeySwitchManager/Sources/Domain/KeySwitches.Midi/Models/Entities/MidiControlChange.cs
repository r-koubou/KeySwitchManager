using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities
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
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class MidiProgramChange : IMidiMessage
    {
        public IMidiMessageData Status { get; }
        public IMidiMessageData DataByte1 { get; }
        public IMidiMessageData DataByte2 { get; }

        public MidiProgramChange( MidiStatus status, MidiProgramChangeNumber number )
        {
            Status    = status;
            DataByte1 = number;
            DataByte2 = GenericMidiData.Empty;
        }
    }
}
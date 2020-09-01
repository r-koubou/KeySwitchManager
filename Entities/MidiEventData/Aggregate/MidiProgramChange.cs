using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData.Aggregate
{
    /// <summary>
    /// Represents a MIDI program change.
    /// </summary>
    public class MidiProgramChange : IMidiEvent
    {
        public IMidiEventData Status { get; } = MidiStatusCode.ProgramChange;
        public IMidiEventData DataByte1 { get; }
        public IMidiEventData DataByte2 { get; }

        public MidiProgramChange( MidiChannel channel, MidiProgramChangeNumber number )
        {
            DataByte1 = channel;
            DataByte2 = number;
        }
    }
}
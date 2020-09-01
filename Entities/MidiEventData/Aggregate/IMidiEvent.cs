using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData.Aggregate
{
    /// <summary>
    /// MIDI event aggregation that makes up the sound slot.
    /// Status bytes are not defined because they are not used.
    /// </summary>
    public interface IMidiEvent
    {
        /// <summary>
        /// MIDI status code
        /// </summary>
        public  IMidiEventData Status { get; }

        /// <summary>
        /// MIDI event: 1st data byte
        /// </summary>

        public  IMidiEventData DataByte1 { get; }
        /// <summary>
        /// MIDI event: 2nd data byte
        /// </summary>
        public  IMidiEventData DataByte2 { get; }
    }
}
namespace ArticulationManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// An interface for MIDI Event data byte
    /// </summary>
    public interface IMessageData
    {
        /// <summary>
        /// MIDI event data values presented as integer values.
        /// </summary>
        public int Value { get; }
    }
}
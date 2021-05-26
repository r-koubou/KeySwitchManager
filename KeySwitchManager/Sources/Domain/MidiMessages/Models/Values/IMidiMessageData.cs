namespace KeySwitchManager.Domain.MidiMessages.Models.Values
{
    /// <summary>
    /// An interface for MIDI Event data byte
    /// </summary>
    public interface IMidiMessageData
    {
        /// <summary>
        /// MIDI event data values presented as integer values.
        /// </summary>
        public int Value { get; }
    }
}
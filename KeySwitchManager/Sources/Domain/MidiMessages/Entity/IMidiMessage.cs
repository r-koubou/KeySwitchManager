using System;

using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages.Entity
{
    /// <summary>
    /// MIDI event aggregation that makes up the sound slot.
    /// Status bytes are not defined because they are not used.
    /// </summary>
    public interface IMidiMessage : IEquatable<IMidiMessage>
    {
        /// <summary>
        /// MIDI status byte
        /// </summary>
        public IMidiMessageData Status { get; }

        /// <summary>
        /// MIDI event: 1st byte in data bytes
        /// </summary>

        public IMidiMessageData DataByte1 { get; }
        /// <summary>
        /// MIDI event: 2nd byte in data bytes
        /// </summary>
        public IMidiMessageData DataByte2 { get; }

        bool IEquatable<IMidiMessage>.Equals( IMidiMessage? other )
        {
            return other != null &&
                   Status.Value == other.Status.Value &&
                   DataByte1.Value == other.DataByte1.Value &&
                   DataByte2.Value == other.DataByte2.Value;
        }
    }
}
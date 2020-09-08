using System;

using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages.Aggregate
{
    /// <summary>
    /// MIDI event aggregation that makes up the sound slot.
    /// Status bytes are not defined because they are not used.
    /// </summary>
    public interface IMessage
    {
        /// <summary>
        /// MIDI status code
        /// </summary>
        public IMessageData Status { get; }

        /// <summary>
        /// MIDI channel code which is included status byte.
        /// Set to Zero if message has no channel data.
        /// </summary>
        public IMessageData Channel { get; }

        /// <summary>
        /// MIDI event: 1st data byte
        /// </summary>

        public IMessageData DataByte1 { get; }
        /// <summary>
        /// MIDI event: 2nd data byte
        /// </summary>
        public IMessageData DataByte2 { get; }
    }
}
using System;
using System.Collections.Generic;

using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Entities
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

        #region IEquatable
        bool IEquatable<IMidiMessage>.Equals( IMidiMessage? other )
        {
            return other != null &&
                   Status.Value == other.Status.Value &&
                   DataByte1.Value == other.DataByte1.Value &&
                   DataByte2.Value == other.DataByte2.Value;
        }
        #endregion

        #region IEqualityComparer
        public static IEqualityComparer<IMidiMessage> EqualityComparer => new DefaultComparer();

        private class DefaultComparer : IEqualityComparer<IMidiMessage>
        {
            public bool Equals( IMidiMessage? x, IMidiMessage? y )
            {
                if( x == null && y == null )
                {
                    return true;
                }
                if( x != null )
                {
                    return x.Equals( y );
                }

                return y != null && y.Equals( x );
            }

            public int GetHashCode( IMidiMessage message )
                => HashCode.Combine(
                    message.Status.Value,
                    message.DataByte1.Value,
                    message.DataByte2.Value
                );
        }
        #endregion
    }
}
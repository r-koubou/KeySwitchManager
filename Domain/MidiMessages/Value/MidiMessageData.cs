using System;

using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public abstract class MidiMessageData
        : IMidiMessageData,
          IEquatable<MidiMessageData>,
          IComparable<MidiMessageData>
    {
        public int Value { get; }

        protected MidiMessageData( int value, int min, int max )
        {
            RangeValidateHelper.ValidateRange( value, min, max );
            Value = value;
        }

        public bool Equals( MidiMessageData? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( MidiMessageData? other )
        {
            if( other == null )
            {
                throw new ArgumentNullException( nameof( other ) );
            }

            if( Value == other.Value )
            {
                return 0;
            }

            if( Value > other.Value )
            {
                return 1;
            }

            return -1;
        }

        public override string ToString() => Value.ToString();

    }
}
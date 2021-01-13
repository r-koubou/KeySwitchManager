using System;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public abstract class MidiMessageData
        : IMidiMessageData,
          IEquatable<MidiMessageData>,
          IComparable<MidiMessageData>
    {
        public int Value { get; }

        protected MidiMessageData( int value )
        {
            Value = value;
        }

        public override string ToString()
            => Value.ToString();

        #region Equality
        public override bool Equals( object? obj )
        {
            if( ReferenceEquals( null, obj ) )
            {
                return false;
            }

            if( ReferenceEquals( this, obj ) )
            {
                return true;
            }

            if( obj.GetType() != this.GetType() )
            {
                return false;
            }

            return Equals( (MidiMessageData)obj );
        }

        public override int GetHashCode()
            => GetType().GetHashCode() + Value.GetHashCode();

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
        #endregion Equality
    }
}
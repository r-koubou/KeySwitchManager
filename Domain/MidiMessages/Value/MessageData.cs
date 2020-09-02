using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.MidiMessages.Value
{
    public abstract class MessageData
        : IMessageData,
          IEquatable<MessageData>,
          IComparable<MessageData>
    {
        public int Value { get; }

        protected MessageData( int value, int min, int max )
        {
            RangeValidateHelper.ValidateRange( value, min, max );
            Value = value;
        }

        public bool Equals( MessageData? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( MessageData? other )
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
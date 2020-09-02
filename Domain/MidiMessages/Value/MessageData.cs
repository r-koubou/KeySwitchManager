using System;
using System.Diagnostics.CodeAnalysis;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.MidiMessages.Value
{
    public abstract class MessageData : IMessageData, IEquatable<MessageData>
    {
        public int Value { get; }

        protected MessageData( int value, int min, int max )
        {
            RangeValidateHelper.ValidateIntRange( value, min, max );
            Value = value;
        }

        public bool Equals( [AllowNull] MessageData other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value.ToString();

    }
}
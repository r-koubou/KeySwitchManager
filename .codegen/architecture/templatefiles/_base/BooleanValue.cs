using System;

namespace Domain
{
    public abstract class BooleanValue : IEquatable<BooleanValue>
    {
        protected bool Value { get; }

        protected BooleanValue( bool value )
        {
            Value = value;
        }

        public override string ToString()
            => Value.ToString();

        #region Equality
        public override int GetHashCode()
            => Value.GetHashCode();

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

            return obj.GetType() == this.GetType() && Equals( (BooleanValue)obj );
        }

        public bool Equals( BooleanValue? other )
        {
            return other?.Value == Value;
        }
        #endregion Equality

        #region Operator
            public static bool operator true( BooleanValue a )
            => a.Value;

        public static bool operator false( BooleanValue a )
            => !a.Value;

        public static bool operator &( BooleanValue a, BooleanValue b )
            => a.Value && b.Value;

        public static bool operator |( BooleanValue a, BooleanValue b )
            => a.Value || b.Value;

        public static bool operator ==( BooleanValue a, BooleanValue b )
            => a.Value && b.Value;

        public static bool operator != ( BooleanValue a, BooleanValue b )
            => !( a == b );
        #endregion
    }
}
using System;

namespace path.to.your.ns
{
    public class __name__ : IEquatable<__name__>, IComparable<__name__>
    {
        public string Value { get; }

        public __name__( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

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

            return Equals( (__name__)obj );
        }

        public override int GetHashCode()
            => GetType().GetHashCode() + Value.GetHashCode();

        public bool Equals( __name__? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( __name__? other )
        {
            if( other == null )
            {
                throw new ArgumentNullException( nameof( other ) );
            }

            return string.Compare( other.Value, Value, StringComparison.Ordinal );
        }
        #endregion Equality
    }

    #region Factory
    public interface I__name__Factory
    {
        public static I__name__Factory Default => new DefaultFactory();

        __name__ Create( string value );

        private class DefaultFactory : I__name__Factory
        {
            public __name__ Create( string value )
            {
                value = StringHelper.ValidateNullOrTrimEmpty( value );
                return new __name__( value );
            }
        }
    }
    #endregion Factory
}
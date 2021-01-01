using System;

namespace path.to.your.ns
{
    public class __name__ : IEquatable<__name__>, IComparable<__name__>
    {
        public int Value { get; }

        public __name__( int value )
        {
            Value = value;
        }

        public override string ToString()
            => Value.ToString();

        #region Equality
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

    #region Factory
    public interface I__name__Factory
    {
        public static I__name__Factory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        __name__ Create( int value );

        private class DefaultFactory : I__name__Factory
        {
            public int MinValue => __minvalue__;
            public int MaxValue => __maxvalue__;

            public __name__ Create( int value )
            {
                value = NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new __name__( value );
            }
        }
    }
    #endregion Factory
}
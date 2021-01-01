using System;

namespace path.to.your.ns
{
    public class __name__ : IEquatable<__name__>
    {
        public bool Value { get; }

        public __name__( bool value )
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
        #endregion Equality
    }

    #region Factory
    public interface I__name__Factory
    {
        public static I__name__Factory Default => new DefaultFactory();

        __name__ Create( bool value );

        private class DefaultFactory : I__name__Factory
        {
            public __name__ Create( bool value )
            {
                return new __name__( value );
            }
        }
    }
    #endregion Factory
}
namespace path.to.your.ns
{
    public class __name__ : IEquatable<__name__>
    {
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

            return obj.GetType() == this.GetType() && Equals( (__name__)obj );
        }

        public bool Equals( __name__? other )
        {
            return other?.Value == Value;
        }
        #endregion Equality

        #region Operator
            public static bool operator true( __name__ a )
            => a.Value;

        public static bool operator false( __name__ a )
            => !a.Value;

        #region Logical and, or
        public static bool operator &( SASA a, SASA b )
            => a.Value && b.Value;

        public static bool operator |( SASA a, SASA b )
            => a.Value || b.Value;

        #endregion

        #region Conditional Operators
        public static bool operator ==( SASA a, SASA b )
            => a.Value && b.Value;

        public static bool operator != ( SASA a, SASA b )
            => !( a == b );

        public static bool operator < ( SASA a, SASA b )
            => a.Value < b.Value;

        public static bool operator > ( SASA a, SASA b )
            => a.Value > b.Value ;

        public static bool operator <= ( SASA a, SASA b )
            => a.Value <= b.Value;

        public static bool operator >= ( SASA a, SASA b )
            => a.Value >= b.Value;

        #endregion Conditional Operators

        #region Binary Operators
        public static SASA operator + ( SASA a, SASA b )
            => a.Value + b.Value;

        public static SASA operator - ( SASA a, SASA b )
            => a.Value + b.Value;

        public static SASA operator * ( SASA a, SASA b )
            => a.Value + b.Value;

        public static SASA operator / ( SASA a, SASA b )
            => a.Value + b.Value;

        public static SASA operator % ( SASA a, SASA b )
            => a.Value + b.Value;

        #endregion Binary Operators

        #endregion Operator
   }
}
using System;

using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Articulation name
    /// </summary>
    public class ArticulationName : IEquatable<ArticulationName>, IComparable<ArticulationName>
    {
        public string Value { get; }

        public ArticulationName( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( ArticulationName? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( ArticulationName? other )
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
    public interface IArticulationNameFactory
    {
        public static IArticulationNameFactory Default => new DefaultFactory();

        ArticulationName Create( string value );

        private class DefaultFactory : IArticulationNameFactory
        {
            public ArticulationName Create( string value )
            {
                StringHelper.ValidateNullOrTrimEmpty<InvalidNameException>( value );
                return new ArticulationName( value );
            }
        }
    }
    #endregion Factory
}

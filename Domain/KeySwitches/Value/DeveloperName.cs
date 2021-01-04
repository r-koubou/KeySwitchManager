using System;

using KeySwitchManager.Domain.Commons;

using RkHelper.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A DeveloperName name
    /// </summary>
    public class DeveloperName : IEquatable<DeveloperName>, IComparable<DeveloperName>
    {
        public string Value { get; }

        public DeveloperName( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( DeveloperName? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( DeveloperName? other )
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
    public interface IDeveloperNameFactory
    {
        public static IDeveloperNameFactory Default => new DefaultFactory();

        DeveloperName Create( string value );

        private class DefaultFactory : IDeveloperNameFactory
        {
            public DeveloperName Create( string value )
            {
                StringHelper.ValidateEmpty( value );
                return new DeveloperName( value );
            }
        }
    }
    #endregion Factory
}

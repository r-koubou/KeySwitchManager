using System;

using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A description of keyswitch
    /// </summary>
    public class Description : IEquatable<Description>, IComparable<Description>
    {
        public string Value { get; }

        public Description( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( Description? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( Description? other )
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
    public interface IDescriptionFactory
    {
        public static IDescriptionFactory Default => new DefaultFactory();

        Description Create( string value );

        private class DefaultFactory : IDescriptionFactory
        {
            public Description Create( string value )
            {
                value = StringHelper.IsNullOrTrimEmpty( value ) ? string.Empty : value;
                return new Description( value );
            }
        }
    }
    #endregion Factory
}
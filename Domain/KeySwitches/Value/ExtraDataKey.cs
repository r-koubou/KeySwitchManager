using System;

using KeySwitchManager.Domain.Commons;

using RkHelper.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraDataKey : IDictionaryKey<string>, IEquatable<ExtraDataKey>
    {
        public string Value { get; }

        public ExtraDataKey( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( ExtraDataKey? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( ExtraDataKey? other )
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
    public interface IExtraDataKeyFactory
    {
        public static IExtraDataKeyFactory Default => new DefaultFactory();

        ExtraDataKey Create( string value );

        private class DefaultFactory : IExtraDataKeyFactory
        {
            public ExtraDataKey Create( string value )
            {
                StringHelper.ValidateEmpty( value );
                return new ExtraDataKey( value );
            }
        }
    }
    #endregion Factory
}
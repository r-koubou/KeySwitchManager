using System;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraDataValue : IDictionaryKey<string>, IEquatable<ExtraDataValue>
    {
        public static readonly ExtraDataValue Empty = new ExtraDataValue();

        public string Value { get; }

        private ExtraDataValue()
        {
            Value = "";
        }

        public ExtraDataValue( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( ExtraDataValue? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( ExtraDataValue? other )
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
    public interface IExtraDataValueFactory
    {
        public static IExtraDataValueFactory Default => new DefaultFactory();

        ExtraDataValue Create( string value );

        private class DefaultFactory : IExtraDataValueFactory
        {
            public ExtraDataValue Create( string value )
            {
                StringHelper.ValidateNullOrTrimEmpty( value );
                return new ExtraDataValue( value );
            }
        }
    }
    #endregion Factory
}
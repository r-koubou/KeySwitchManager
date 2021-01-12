using System;

using KeySwitchManager.Domain.Commons;

using RkHelper.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// An Instrument name
    /// </summary>
    public class InstrumentName : IEquatable<InstrumentName>, IComparable<InstrumentName>
    {
        public string Value { get; }

        public InstrumentName( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( InstrumentName? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( InstrumentName? other )
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
    public interface IInstrumentNameFactory
    {
        public static IInstrumentNameFactory Default => new DefaultFactory();

        InstrumentName Create( string value );

        private class DefaultFactory : IInstrumentNameFactory
        {
            public InstrumentName Create( string value )
            {
                StringHelper.ValidateEmpty( value );
                return new InstrumentName( value );
            }
        }
    }
    #endregion Factory
}
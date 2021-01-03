using System;

using KeySwitchManager.Common.Exceptions;
using KeySwitchManager.Common.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A ProductName name
    /// </summary>
    public class ProductName : IEquatable<ProductName>, IComparable<ProductName>
    {
        public string Value { get; }

        public ProductName( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( ProductName? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( ProductName? other )
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
    public interface IProductNameFactory
    {
        public static IProductNameFactory Default => new DefaultFactory();

        ProductName Create( string value );

        private class DefaultFactory : IProductNameFactory
        {
            public ProductName Create( string value )
            {
                StringHelper.ValidateNullOrTrimEmpty<InvalidNameException>( value );
                return new ProductName( value );
            }
        }
    }
    #endregion Factory
}

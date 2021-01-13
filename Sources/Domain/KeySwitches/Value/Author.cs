using System;

using RkHelper.Text;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    /// <summary>
    /// A created author name
    /// </summary>
    public class Author : IEquatable<Author>, IComparable<Author>
    {
        public string Value { get; }

        public Author( string value )
        {
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality
        public bool Equals( Author? other )
        {
            return other != null && other.Value == Value;
        }

        public int CompareTo( Author? other )
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
    public interface IAuthorFactory
    {
        public static IAuthorFactory Default => new DefaultFactory();

        Author Create( string value );

        private class DefaultFactory : IAuthorFactory
        {
            public Author Create( string value )
            {
                value = StringHelper.IsEmpty( value ) ? string.Empty : value;
                return new Author( value );
            }
        }
    }
    #endregion Factory
}
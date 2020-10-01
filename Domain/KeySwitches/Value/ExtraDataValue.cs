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
            StringHelper.ValidateNullOrTrimEmpty( value );
            Value = value;
        }

        public override string ToString() => Value;

        #region Equality Members
        public bool Equals( ExtraDataValue? other )
        {
            if( ReferenceEquals( null, other ) )
            {
                return false;
            }

            if( ReferenceEquals( this, other ) )
            {
                return true;
            }

            return Value == other.Value;
        }

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

            if( obj.GetType() != this.GetType() )
            {
                return false;
            }

            return Equals( (ExtraDataValue)obj );
        }

        public override int GetHashCode() => Value.GetHashCode();
        #endregion
    }
}
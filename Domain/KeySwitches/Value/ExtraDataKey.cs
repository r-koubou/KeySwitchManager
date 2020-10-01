using System;

using KeySwitchManager.Common.Text;
using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraDataKey : IDictionaryKey<string>, IEquatable<ExtraDataKey>
    {
        public string Value { get; }

        public ExtraDataKey( string key )
        {
            StringHelper.ValidateNullOrTrimEmpty( key );
            Value = key;
        }

        public override string ToString() => Value;

        #region Equality Members
        public bool Equals( ExtraDataKey? other )
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

            return Equals( (ExtraDataKey)obj );
        }

        public override int GetHashCode() => Value.GetHashCode();
        #endregion
    }
}
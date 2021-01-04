using System;

using RkHelper.Text;

namespace KeySwitchManager.Domain.Commons
{
    public class EntityGuid : IEquatable<EntityGuid>
    {
        public Guid Value { get; }

        public EntityGuid()
        {
            Value = Guid.NewGuid();
        }

        public EntityGuid( string value )
        {
            StringHelper.ValidateEmpty( value );
            Value = Guid.Parse( value );
        }

        public EntityGuid( Guid value )
        {
            Value = value;
        }

        public bool Equals( EntityGuid? other )
        {
            return other != null && Value.Equals( other.Value );
        }

        public override string ToString() => Value.ToString( "D" );
    }
}
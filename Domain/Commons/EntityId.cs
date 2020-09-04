using System;

namespace ArticulationManager.Domain.Commons
{
    public class EntityId : IEquatable<EntityId>
    {
        public ulong Value { get; }

        public static readonly EntityId Zero = new EntityId( 0UL );
        public static readonly EntityId Default = Zero;

        public EntityId( ulong value )
        {
            // Accept any value
            Value = value;
        }

        public bool Equals( EntityId? other )
        {
            return other != null && Value == other.Value;
        }
    }
}
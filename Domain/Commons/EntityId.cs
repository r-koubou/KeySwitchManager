using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Commons
{
    public class EntityId : IEquatable<EntityId>
    {
        public string Value { get; }

        public EntityId( string value )
        {
            StringHelper.ValidateNullOrTrimEmpty( value );
            Value = value;
        }

        public bool Equals( EntityId? other )
        {
            return other != null && Value == other.Value;
        }
    }
}
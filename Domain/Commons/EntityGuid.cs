using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Commons
{
    public class EntityGuid : IEquatable<EntityGuid>
    {
        public string Value { get; }

        public EntityGuid( string value )
        {
            StringHelper.ValidateNullOrTrimEmpty( value );
            Value = value;
        }

        public bool Equals( EntityGuid? other )
        {
            return other != null && Value == other.Value;
        }
    }
}
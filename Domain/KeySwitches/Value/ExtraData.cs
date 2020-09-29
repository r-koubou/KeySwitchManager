using System;
using System.Collections.Generic;
using System.Linq;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ExtraData : IEquatable<ExtraData>
    {
        private IReadOnlyDictionary<ExtraDataKey, ExtraDataValue> Value { get; }

        public ExtraDataValue this[ ExtraDataKey key ] => Value[ key ];
        public IEnumerable<ExtraDataKey> Keys => Value.Keys;
        public IEnumerable<ExtraDataValue> Values => Value.Values;

        public ExtraData( IReadOnlyDictionary<ExtraDataKey, ExtraDataValue> value )
        {
            Value = value;
        }

        public bool Equals( ExtraData? other )
        {
            return other != null && other.Value.SequenceEqual( Value );
        }
    }
}
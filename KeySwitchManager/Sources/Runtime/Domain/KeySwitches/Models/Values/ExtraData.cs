using System;
using System.Collections.Generic;
using System.Linq;

using KeySwitchManager.Commons.Data;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    public class ExtraData : DataDictionary<ExtraDataKey, ExtraDataValue>, IEquatable<ExtraData>
    {
        public ExtraData()
        {}

        public ExtraData( IReadOnlyDictionary<ExtraDataKey, ExtraDataValue> dictionary )
            : base( dictionary )
        {}

        public bool Equals( ExtraData? other )
        {
            return other != null && other.Dictionary.SequenceEqual( Dictionary );
        }
    }
}
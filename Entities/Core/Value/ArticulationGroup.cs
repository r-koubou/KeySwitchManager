using System;
using System.Diagnostics.CodeAnalysis;

using ArticulationManager.Utilities;

namespace ArticulationManager.Entities.Core.Value
{
    public class ArticulationGroup : IEquatable<ArticulationGroup>
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public int Value { get; }

        public ArticulationGroup( int groupValue )
        {
            RangeValidateHelper.ValidateIntRange( groupValue, MinValue, MaxValue );
            Value = groupValue;
        }

        public bool Equals( [AllowNull] ArticulationGroup other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value.ToString();
    }
}
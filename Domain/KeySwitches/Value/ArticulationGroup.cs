using System;

using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Domain.KeySwitches.Value
{
    public class ArticulationGroup : IEquatable<ArticulationGroup>
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public int Value { get; }

        public ArticulationGroup( int groupValue )
        {
            RangeValidateHelper.ValidateRange( groupValue, MinValue, MaxValue );
            Value = groupValue;
        }

        public bool Equals( ArticulationGroup? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value.ToString();
    }
}
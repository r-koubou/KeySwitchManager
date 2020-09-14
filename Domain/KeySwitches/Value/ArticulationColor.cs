using System;

using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.KeySwitches.Value
{
    public class ArticulationColor : IEquatable<ArticulationColor>
    {
        public const int MinValue = 0;
        public const int MaxValue = 255;
        public int Value { get; }

        public ArticulationColor( int colorValue )
        {
            RangeValidateHelper.ValidateRange( colorValue, MinValue, MaxValue );
            Value = colorValue;
        }

        public bool Equals( ArticulationColor? other )
        {
            return other != null && other.Value == Value;
        }

        public override string ToString() => Value.ToString();
    }
}
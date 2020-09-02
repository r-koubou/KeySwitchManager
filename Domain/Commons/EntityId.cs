using ArticulationManager.Common.Utilities;

namespace ArticulationManager.Domain.Commons
{
    public class EntityId
    {
        public const ulong MinValue = ulong.MinValue;

        public const ulong MaxValue = ulong.MaxValue;

        public ulong Value { get; }

        public EntityId( ulong value )
        {
            RangeValidateHelper.ValidateRange( value, MinValue, MaxValue );
            Value = value;
        }
    }
}
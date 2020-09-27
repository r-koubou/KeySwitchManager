using System;

using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Gateways.KeySwitches.Value
{
    public class SaveResult : IEquatable<SaveResult>
    {
        private const int MinValue = 0;
        private const int MaxValue = int.MaxValue - 1;

        public int Inserted { get; }
        public int Updated { get; }

        public bool Any =>
            Inserted > MinValue &&
            Updated > MinValue;

        public SaveResult( int inserted, int updated )
        {
            RangeValidateHelper.ValidateRange( inserted, MinValue, MaxValue );
            RangeValidateHelper.ValidateRange( updated,  MinValue, MaxValue );
            Inserted = inserted;
            Updated  = updated;
        }

        public bool Equals( SaveResult? other )
        {
            return other != null &&
                   other.Inserted == Inserted &&
                   other.Updated == Updated;
        }
    }
}
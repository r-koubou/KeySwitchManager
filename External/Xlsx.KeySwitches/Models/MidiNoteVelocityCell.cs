using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.Xlsx.KeySwitches.Models
{
    public class MidiNoteVelocityCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiNoteVelocityCell( int velocity )
        {
            RangeValidateHelper.ValidateRange( velocity, MinValue, MaxValue );
            Value = velocity;
        }
    }
}
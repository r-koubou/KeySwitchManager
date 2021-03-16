using RkHelper.Number;

namespace KeySwitchManager.Xlsx.KeySwitch.Model
{
    public class MidiNoteVelocityCell
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public int Value { get; }

        public MidiNoteVelocityCell( int velocity )
        {
            NumberHelper.ValidateRange( velocity, MinValue, MaxValue );
            Value = velocity;
        }
    }
}
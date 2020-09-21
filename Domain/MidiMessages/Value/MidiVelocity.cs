namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiVelocity : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public MidiVelocity( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
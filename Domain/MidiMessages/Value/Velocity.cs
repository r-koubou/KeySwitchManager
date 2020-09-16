namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class Velocity : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public Velocity( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
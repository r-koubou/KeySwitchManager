namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class LeastSignificantByte : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public LeastSignificantByte( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
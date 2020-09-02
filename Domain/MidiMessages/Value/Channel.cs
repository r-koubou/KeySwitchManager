namespace ArticulationManager.Domain.MidiMessages.Value
{
    public class Channel : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;

        public Channel( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiChannel : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;

        public MidiChannel( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
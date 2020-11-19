namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiChannel : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;

        public static readonly  MidiChannel Zero = new MidiChannel( MinValue );

        public MidiChannel( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
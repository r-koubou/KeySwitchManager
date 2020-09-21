namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiNoteNumber : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public MidiNoteNumber( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
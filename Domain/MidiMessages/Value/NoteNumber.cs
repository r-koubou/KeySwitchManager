namespace ArticulationManager.Domain.MidiMessages.Value
{
    public class NoteNumber :MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public NoteNumber( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
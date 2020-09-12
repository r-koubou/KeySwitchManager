namespace ArticulationManager.Domain.MidiMessages.Value
{
    public class ProgramChangeNumber : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public ProgramChangeNumber( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
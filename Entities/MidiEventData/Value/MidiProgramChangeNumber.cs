namespace ArticulationManager.Entities.MidiEventData.Value
{
    public class MidiProgramChangeNumber : MidiEventData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x0F;

        public MidiProgramChangeNumber( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
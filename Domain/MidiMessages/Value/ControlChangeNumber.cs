namespace ArticulationManager.Domain.MidiMessages.Value
{
    public class ControlChangeNumber : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0x7F;

        public ControlChangeNumber( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
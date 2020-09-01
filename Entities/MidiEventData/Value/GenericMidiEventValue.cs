namespace ArticulationManager.Entities.MidiEventData.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class GenericMidiEventValue : MidiEventData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        public static readonly GenericMidiEventValue Zero = new GenericMidiEventValue( 0 );

        public GenericMidiEventValue( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
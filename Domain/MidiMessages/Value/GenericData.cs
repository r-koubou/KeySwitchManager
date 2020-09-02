namespace ArticulationManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class GenericData : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        public static readonly GenericData Zero = new GenericData( 0 );

        public GenericData( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
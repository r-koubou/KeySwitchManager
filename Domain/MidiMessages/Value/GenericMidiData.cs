namespace KeySwitchManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class GenericMidiData : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        public static readonly GenericMidiData Empty = new GenericMidiData( 0 );
        public static readonly GenericMidiData Zero = new GenericMidiData( 0 );

        public GenericMidiData( int value )
            : base( value, MinValue, MaxValue )
        {}
    }
}
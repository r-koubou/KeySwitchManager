namespace ArticulationManager.Entities.MidiEventData.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class MidiStatusCode : MidiEventData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        #region Presets
        public static readonly MidiStatusCode Zero = new MidiStatusCode( 0 );
        public static readonly MidiStatusCode NoteOn = new MidiStatusCode( 0x90 );
        public static readonly MidiStatusCode ControlChange = new MidiStatusCode( 0xB0 );
        public static readonly MidiStatusCode ProgramChange = new MidiStatusCode( 0xC0 );
        #endregion

        public MidiStatusCode( int value )
            : base( value, MinValue, MaxValue )
        {}
        public MidiStatusCode( MidiChannel channel, int value )
            : base( channel.Value + value, MinValue, MaxValue )
        {}
    }
}
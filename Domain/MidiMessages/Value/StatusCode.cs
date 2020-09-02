namespace ArticulationManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class StatusCode : MessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        #region Presets
        public static readonly StatusCode NoteOn = new StatusCode( 0x90 );
        public static readonly StatusCode ControlChange = new StatusCode( 0xB0 );
        public static readonly StatusCode ProgramChange = new StatusCode( 0xC0 );
        #endregion

        public StatusCode( int value )
            : base( value, MinValue, MaxValue )
        {}
        public StatusCode( Channel channel, int value )
            : base( channel.Value + value, MinValue, MaxValue )
        {}
    }
}
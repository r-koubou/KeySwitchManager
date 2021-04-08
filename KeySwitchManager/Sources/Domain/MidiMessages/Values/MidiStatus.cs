using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Values
{
    /// <summary>
    /// Represents a MIDI status byte
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class MidiStatus : IMidiMessageData
    {
        /// <summary>
        /// Create message as Channel Voice or Channel Mode Message
        /// </summary>
        public MidiStatus( int value, int channel ) : this( value | ( channel & 0x0F ) )
        {}
    }
}
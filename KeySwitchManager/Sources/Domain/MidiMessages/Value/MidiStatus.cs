using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class MidiStatus : IMidiMessageData
    {
        public MidiStatus( int value, int channel ) : this( value | ( channel & 0x0F ) )
        {}
    }
}
using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Values
{
    /// <summary>
    /// Represents a MIDI status byte
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class MidiStatus : IMidiMessageData
    {
        public MidiStatus( int value, int channel = 0x00 ) : this( value | ( channel & 0x0F ) )
        {}
    }
}
using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Models.Values
{
    /// <summary>
    /// Represents a MIDI status byte
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class MidiStatus : IMidiMessageData
    {
        public MidiStatus( int value, MidiChannel channel ) : this( value | channel.Value )
        {}
    }
}
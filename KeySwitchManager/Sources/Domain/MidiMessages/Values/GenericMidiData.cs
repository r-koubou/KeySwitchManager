using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Values
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class GenericMidiData : IMidiMessageData {}
}
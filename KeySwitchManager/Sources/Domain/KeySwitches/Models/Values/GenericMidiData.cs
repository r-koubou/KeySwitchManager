using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Models.Values
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0xFF )]
    public partial class GenericMidiData : IMidiMessageData {}
}
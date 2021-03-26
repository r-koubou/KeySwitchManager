using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Values
{
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0x7F )]
    public partial class MidiNoteNumber : IMidiMessageData {}
}
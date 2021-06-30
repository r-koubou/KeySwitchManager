using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Models.Values
{
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0x7F )]
    public partial class MidiNoteNumber : IMidiMessageData {}
}
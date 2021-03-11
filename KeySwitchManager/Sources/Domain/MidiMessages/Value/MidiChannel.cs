using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0x0F )]
    public partial class MidiChannel : IMidiMessageData {}
}
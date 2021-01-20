using ValueObjectGenerator;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    [ValueObject( typeof( int ) )]
    [ValueRange( 0x00, 0x7F )]
    public partial class MidiProgramChangeNumber : IMidiMessageData {}
}
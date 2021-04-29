using ValueObjectGenerator;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Values
{
    [ValueObject(typeof(int))]
    [ValueRange(0x00,0x7F)]
    public partial class MidiControlChangeNumber : IMidiMessageData {}
}
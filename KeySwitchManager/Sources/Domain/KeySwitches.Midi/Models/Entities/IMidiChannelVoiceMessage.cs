using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities
{
    public interface IMidiChannelVoiceMessage : IMidiMessage
    {
        public IMidiMessageData Channel { get; }
    }
}

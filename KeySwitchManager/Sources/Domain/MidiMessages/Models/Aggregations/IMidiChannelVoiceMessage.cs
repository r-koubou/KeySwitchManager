using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Aggregations
{
    public interface IMidiChannelVoiceMessage : IMidiMessage
    {
        public IMidiMessageData Channel { get; }
    }
}

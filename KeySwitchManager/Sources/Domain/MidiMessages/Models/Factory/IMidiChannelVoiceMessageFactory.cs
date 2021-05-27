using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;

namespace KeySwitchManager.Domain.MidiMessages.Models.Factory
{
    public interface IMidiChannelVoiceMessageFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessage
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
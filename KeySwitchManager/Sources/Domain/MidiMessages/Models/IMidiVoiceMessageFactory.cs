using KeySwitchManager.Domain.MidiMessages.Models.Entities;

namespace KeySwitchManager.Domain.MidiMessages.Models
{
    public interface IMidiChannelVoiceMessageFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessage
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
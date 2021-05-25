using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models
{
    public interface IMidiChannelVoiceMessageFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessage
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
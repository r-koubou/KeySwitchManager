using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public interface IMidiChannelVoiceMessageModelFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessageModel
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
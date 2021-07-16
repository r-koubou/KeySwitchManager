using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Factory
{
    public interface IMidiChannelVoiceMessageModelFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessageModel
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
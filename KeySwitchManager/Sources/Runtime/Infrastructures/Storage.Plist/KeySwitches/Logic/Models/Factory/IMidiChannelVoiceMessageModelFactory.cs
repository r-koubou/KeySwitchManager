using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Factory
{
    public interface IMidiChannelVoiceMessageModelFactory<out TMidiMessage>
        where TMidiMessage : IMidiChannelVoiceMessageModel
    {
        public TMidiMessage Create( int channel, int data1, int data2 );
    }
}
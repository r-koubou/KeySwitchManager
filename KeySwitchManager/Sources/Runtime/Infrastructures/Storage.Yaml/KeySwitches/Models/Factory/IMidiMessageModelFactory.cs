using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Factory
{
    public interface IMidiMessageModelFactory<out TMidiMessage> where TMidiMessage : IMidiMessageModel
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
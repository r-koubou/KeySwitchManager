using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public interface IMidiMessageModelFactory<out TMidiMessage> where TMidiMessage : IMidiMessageModel
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
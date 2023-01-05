using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Factory
{
    public interface IMidiMessageModelFactory<out TMidiMessage> where TMidiMessage : IMidiMessageModel
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
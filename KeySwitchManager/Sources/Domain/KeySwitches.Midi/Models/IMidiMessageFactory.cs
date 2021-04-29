using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models
{
    public interface IMidiMessageFactory<out TMidiMessage> where TMidiMessage : IMidiMessage
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
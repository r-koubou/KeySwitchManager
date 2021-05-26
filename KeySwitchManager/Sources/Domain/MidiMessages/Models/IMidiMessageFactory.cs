using KeySwitchManager.Domain.MidiMessages.Models.Entities;

namespace KeySwitchManager.Domain.MidiMessages.Models
{
    public interface IMidiMessageFactory<out TMidiMessage> where TMidiMessage : IMidiMessage
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
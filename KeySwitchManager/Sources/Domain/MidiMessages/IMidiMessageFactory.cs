using KeySwitchManager.Domain.MidiMessages.Entity;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiMessageFactory<out TMidiMessage> where TMidiMessage : IMidiMessage
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
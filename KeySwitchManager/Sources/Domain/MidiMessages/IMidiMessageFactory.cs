using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiMessageFactory<out TMidiMessage> where TMidiMessage : IMidiMessage
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
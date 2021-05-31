using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;

namespace KeySwitchManager.Domain.MidiMessages.Models.Factory
{
    public interface IMidiMessageFactory<out TMidiMessage> where TMidiMessage : IMidiMessage
    {
        public TMidiMessage Create( int status, int data1, int data2 );
    }
}
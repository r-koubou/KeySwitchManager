using KeySwitchManager.Domain.MidiMessages.Aggregate;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiMessageFactory
    {
        public IMidiMessage Create( int status, int channel, int data1, int data2 );
    }
}
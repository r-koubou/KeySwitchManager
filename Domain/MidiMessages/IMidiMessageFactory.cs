using ArticulationManager.Domain.MidiMessages.Aggregate;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IMidiMessageFactory
    {
        public IMessage Create( int status, int channel, int data1, int data2 );
    }
}
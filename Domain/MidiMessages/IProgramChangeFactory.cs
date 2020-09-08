using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IProgramChangeFactory : IMidiMessageFactory
    {
        public ProgramChange Create( int pcNumber );
        public ProgramChange Create( int channel, int pcNumber );

        public class DefaultFactory : IProgramChangeFactory
        {
            public IMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1 );
            }

            public ProgramChange Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public ProgramChange Create( int channel, int pcNumber )
            {
                return new ProgramChange(
                    new MidiChannel( channel ),
                    new ProgramChangeNumber( pcNumber )
                );
            }
        }
    }
}
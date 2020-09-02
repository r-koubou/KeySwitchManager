using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IProgramChangeFactory
    {
        public ProgramChange Create( int pcNumber );
        public ProgramChange Create( int channel, int pcNumber );

        public class DefaultFactory : IProgramChangeFactory
        {
            public ProgramChange Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public ProgramChange Create( int channel, int pcNumber )
            {
                return new ProgramChange(
                    new Channel( channel ),
                    new ProgramChangeNumber( pcNumber )
                );
            }
        }
    }
}
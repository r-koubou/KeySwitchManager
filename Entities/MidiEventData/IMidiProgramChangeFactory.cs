using ArticulationManager.Entities.MidiEventData.Aggregate;
using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData
{
    public interface IMidiProgramChangeFactory
    {
        public MidiProgramChange Create( int pcNumber );
        public MidiProgramChange Create( int channel, int pcNumber );

        public class DefaultFactory : IMidiProgramChangeFactory
        {
            public MidiProgramChange Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public MidiProgramChange Create( int channel, int pcNumber )
            {
                return new MidiProgramChange(
                    new MidiChannel( channel ),
                    new MidiProgramChangeNumber( pcNumber )
                );
            }
        }
    }
}
using ArticulationManager.Entities.MidiEventData.Aggregate;
using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData
{
    public interface IMidiNoteOnFactory
    {
        public MidiNoteOn Create( int noteNumber, int velocity );
        public MidiNoteOn Create( int channel, int noteNumber, int velocity );

        public class DefaultFactory : IMidiNoteOnFactory
        {
            public MidiNoteOn Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public MidiNoteOn Create( int channel, int noteNumber, int velocity )
            {
                return new MidiNoteOn(
                    new MidiChannel( channel ),
                    new MidiNoteNumber( noteNumber ),
                    new MidiVelocity( velocity )
                );
            }
        }
    }
}
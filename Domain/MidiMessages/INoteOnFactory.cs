using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface INoteOnFactory
    {
        public NoteOn Create( int noteNumber, int velocity );
        public NoteOn Create( int channel, int noteNumber, int velocity );

        public class DefaultFactory : INoteOnFactory
        {
            public NoteOn Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public NoteOn Create( int channel, int noteNumber, int velocity )
            {
                return new NoteOn( new Channel( channel ), new NoteNumber( noteNumber ), new Velocity( velocity ) );
            }
        }
    }
}
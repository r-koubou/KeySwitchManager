using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface INoteOnFactory
    {
        public NoteOn Create( ulong id, int noteNumber, int velocity );
        public NoteOn Create( ulong id, int channel, int noteNumber, int velocity );

        public class DefaultFactory : INoteOnFactory
        {
            public NoteOn Create( ulong id, int noteNumber, int velocity )
            {
                return  Create( id, 0x00, noteNumber, velocity );
            }

            public NoteOn Create( ulong id, int channel, int noteNumber, int velocity )
            {
                return new NoteOn( new EntityId( id ), new Channel( channel ), new NoteNumber( noteNumber ), new Velocity( velocity ) );
            }
        }
    }
}
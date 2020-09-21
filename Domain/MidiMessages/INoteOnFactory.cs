using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface INoteOnFactory : IMidiMessageFactory
    {
        public NoteOn Create( int noteNumber, int velocity );
        public NoteOn Create( int channel, int noteNumber, int velocity );

        public static INoteOnFactory Default => new DefaultFactory();

        private class DefaultFactory : INoteOnFactory
        {
            public IMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1, data2 );
            }

            public NoteOn Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public NoteOn Create( int channel, int noteNumber, int velocity )
            {
                return new NoteOn( new MidiChannel( channel ), new NoteNumber( noteNumber ), new Velocity( velocity ) );
            }
        }
    }
}
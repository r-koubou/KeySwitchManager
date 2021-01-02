using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Helpers;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiNoteOnFactory : IMidiMessageFactory<MidiNoteOn>
    {
        public MidiNoteOn Create( int noteNumber, int velocity );

        public static IMidiNoteOnFactory Default => new DefaultFactory();

        public static MidiNoteOn Zero =>
            new MidiNoteOn(
                IMidiStatusFactory.Default.Create( MidiStatusHelper.NoteOn ),
                IMidiNoteNumberFactory.Default.Create( 0 ),
                IMidiVelocityFactory.Default.Create( 0 )
            );

        private class DefaultFactory : IMidiNoteOnFactory
        {
            public MidiNoteOn Create( int noteNumber, int velocity )
            {
                return  Create( MidiStatusHelper.NoteOn, noteNumber, velocity );
            }

            public MidiNoteOn Create( int status, int data1, int data2 )
            {
                return new MidiNoteOn(
                    IMidiStatusFactory.Default.Create( status ),
                    IMidiNoteNumberFactory.Default.Create( data1 ),
                    IMidiVelocityFactory.Default.Create( data2 )
                );
            }
        }
    }
}
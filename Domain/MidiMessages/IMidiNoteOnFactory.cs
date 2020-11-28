using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiNoteOnFactory : IMidiMessageFactory
    {
        public MidiNoteOn Create( int noteNumber, int velocity );
        public MidiNoteOn Create( int channel, int noteNumber, int velocity );

        public static IMidiNoteOnFactory Default => new DefaultFactory();

        public static MidiNoteOn Zero =>
            new MidiNoteOn(
                new MidiChannel( 0 ),
                new MidiNoteNumber( 0 ),
                new MidiVelocity( 0 )
            );

        private class DefaultFactory : IMidiNoteOnFactory
        {
            public IMidiMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1, data2 );
            }

            public MidiNoteOn Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public MidiNoteOn Create( int channel, int noteNumber, int velocity )
            {
                return new MidiNoteOn( new MidiChannel( channel ), new MidiNoteNumber( noteNumber ), new MidiVelocity( velocity ) );
            }
        }
    }
}
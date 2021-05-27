using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Aggregations
{
    public interface IMidiNoteOnFactory : IMidiChannelVoiceMessageFactory<MidiNoteOn>
    {
        public MidiNoteOn Create( int noteNumber, int velocity );

        public static IMidiNoteOnFactory Default => new DefaultFactory();

        public static MidiNoteOn Zero =>
            new MidiNoteOn(
                new MidiChannel( 0 ),
                new MidiNoteNumber( 0 ),
                new MidiVelocity( 0 )
            );

        private class DefaultFactory : IMidiNoteOnFactory
        {
            public MidiNoteOn Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public MidiNoteOn Create( int channel, int data1, int data2 )
            {
                return new MidiNoteOn(
                    new MidiChannel( channel ),
                    new MidiNoteNumber( data1 ),
                    new MidiVelocity( data2 )
                );
            }
        }
    }
}
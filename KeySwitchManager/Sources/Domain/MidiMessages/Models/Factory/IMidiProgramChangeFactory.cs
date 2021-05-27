using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Factory
{
    public interface IMidiProgramChangeFactory : IMidiChannelVoiceMessageFactory<MidiProgramChange>
    {
        public MidiProgramChange Create( int pcNumber );
        public MidiProgramChange Create( int channel, int pcNumber );

        public static IMidiProgramChangeFactory Default => new DefaultFactory();

        public static MidiProgramChange Zero =>
            new MidiProgramChange(
                new MidiChannel( 0 ),
                new MidiProgramChangeNumber( 0 )
            );

        private class DefaultFactory : IMidiProgramChangeFactory
        {
            public MidiProgramChange Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public MidiProgramChange Create( int channel, int pcNumber )
            {
                return Create( channel, pcNumber, 0x00 );
            }

            public MidiProgramChange Create( int channel, int data1, int data2 )
            {
                return new MidiProgramChange(
                    new MidiChannel( channel ),
                    new MidiProgramChangeNumber( data1 )
                );
            }
        }
    }
}
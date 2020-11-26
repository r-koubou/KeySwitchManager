using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiProgramChangeFactory : IMidiMessageFactory
    {
        public MidiProgramChange Create( int channel, int pcNumber );

        public static IMidiProgramChangeFactory Default => new DefaultFactory();

        private class DefaultFactory : IMidiProgramChangeFactory
        {
            public IMidiMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1 );
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
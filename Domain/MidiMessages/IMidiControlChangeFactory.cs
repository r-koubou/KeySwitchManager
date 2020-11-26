using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiControlChangeFactory : IMidiMessageFactory
    {
        public MidiControlChange Create( int channel, int ccNumber, int ccValue );

        public static IMidiControlChangeFactory Default => new DefaultFactory();

        public static MidiControlChange Zero =>
            new MidiControlChange(
                new MidiChannel( 0 ),
                new MidiControlChangeNumber( 0 ),
                new MidiControlChangeValue( 0 )
            );

        private class DefaultFactory : IMidiControlChangeFactory
        {
            public IMidiMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1, data2 );
            }

            public MidiControlChange Create( int channel, int ccNumber, int ccValue )
            {
                return new MidiControlChange( new MidiChannel( channel ), new MidiControlChangeNumber( ccNumber ), new MidiControlChangeValue( ccValue ) );
            }
        }
    }
}
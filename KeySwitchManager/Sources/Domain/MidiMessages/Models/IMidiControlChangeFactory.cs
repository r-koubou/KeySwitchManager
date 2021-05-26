using KeySwitchManager.Domain.MidiMessages.Models.Entities;
using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models
{
    public interface IMidiControlChangeFactory : IMidiChannelVoiceMessageFactory<MidiControlChange>
    {
        public MidiControlChange Create( int ccNumber, int ccValue );

        public static IMidiControlChangeFactory Default => new DefaultFactory();

        public static MidiControlChange Zero =>
            new MidiControlChange(
                new MidiChannel( 0 ),
                new MidiControlChangeNumber( 0 ),
                new MidiControlChangeValue( 0 )
            );

        private class DefaultFactory : IMidiControlChangeFactory
        {
            public MidiControlChange Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public MidiControlChange Create( int channel, int data1, int data2 )
            {
                return new MidiControlChange(
                    new MidiChannel( channel ),
                    new MidiControlChangeNumber( data1 ),
                    new MidiControlChangeValue( data2 )
                );
            }
        }
    }
}
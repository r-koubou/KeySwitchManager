using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IGenericMidiMessageFactory : IMidiMessageFactory
    {
        public static IGenericMidiMessageFactory Default => new DefaultFactory();

        public static GenericMidiMessage Zero =>
            new GenericMidiMessage(
                new MidiStatusCode( 0 ),
                new MidiChannel( 0 ),
                new GenericMidiData( 0 ),
                new GenericMidiData( 0 )
            );

        private class DefaultFactory : IGenericMidiMessageFactory
        {
            public IMidiMessage Create( int status, int channel, int data1, int data2 )
            {
                return new GenericMidiMessage(
                    new MidiStatusCode( status ),
                    new MidiChannel( channel ),
                    new GenericMidiData( data1 ),
                    new GenericMidiData( data2 )
                );
            }
        }
    }
}
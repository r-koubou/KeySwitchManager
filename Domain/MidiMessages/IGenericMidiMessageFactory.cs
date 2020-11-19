using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IGenericMidiMessageFactory : IMidiMessageFactory
    {
        public GenericMidiMessage Create( int status, int data1, int data2 );

        public static IGenericMidiMessageFactory Default => new DefaultFactory();

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

            public GenericMidiMessage Create( int status, int data1, int data2 )
            {
                return new GenericMidiMessage(
                    new MidiStatusCode( status ),
                    MidiChannel.Zero,
                    new GenericMidiData( data1 ),
                    new GenericMidiData( data2 )
                );
            }
        }
    }
}
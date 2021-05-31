using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Domain.MidiMessages.Models.Aggregations;
using KeySwitchManager.Domain.MidiMessages.Models.Values;

namespace KeySwitchManager.Domain.MidiMessages.Models.Factory
{
    public interface IGenericMidiMessageFactory : IMidiMessageFactory<GenericMidiMessage>
    {
        public static IGenericMidiMessageFactory Default => new DefaultFactory();

        public static GenericMidiMessage Zero =>
            new GenericMidiMessage(
                new MidiStatus( 0 ),
                new GenericMidiData( 0 ),
                new GenericMidiData( 0 )
            );

        private class DefaultFactory : IGenericMidiMessageFactory
        {
            public GenericMidiMessage Create( int status, int data1, int data2 )
            {
                return new GenericMidiMessage(
                    new MidiStatus( status ),
                     new GenericMidiData( data1 ),
                    new GenericMidiData( data2 )
                );
            }
        }
    }
}
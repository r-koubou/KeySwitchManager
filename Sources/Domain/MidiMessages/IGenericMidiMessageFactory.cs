using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IGenericMidiMessageFactory : IMidiMessageFactory<GenericMidiMessage>
    {
        public static IGenericMidiMessageFactory Default => new DefaultFactory();

        public static GenericMidiMessage Zero =>
            new GenericMidiMessage(
                IMidiStatusFactory.Default.Create( 0 ),
                IGenericMidiDataFactory.Default.Create( 0 ),
                IGenericMidiDataFactory.Default.Create( 0 )
            );

        private class DefaultFactory : IGenericMidiMessageFactory
        {
            public GenericMidiMessage Create( int status, int data1, int data2 )
            {
                return new GenericMidiMessage(
                    IMidiStatusFactory.Default.Create( status ),
                     IGenericMidiDataFactory.Default.Create( data1 ),
                    IGenericMidiDataFactory.Default.Create( data2 )
                );
            }
        }
    }
}
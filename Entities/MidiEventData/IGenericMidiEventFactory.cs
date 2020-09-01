using ArticulationManager.Entities.MidiEventData.Aggregate;
using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData
{
    public interface IGenericMidiEventFactory
    {
        public GenericMidiEvent Create( int status, int data1, int data2 );

        public class DefaultFactory : IGenericMidiEventFactory
        {
            public GenericMidiEvent Create( int status, int data1, int data2 )
            {
                return new GenericMidiEvent(
                    new MidiStatusCode( status ),
                    new GenericMidiEventValue( data1 ),
                    new GenericMidiEventValue( data2 )
                );
            }
        }
    }
}
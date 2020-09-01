using ArticulationManager.Entities.MidiEventData.Aggregate;
using ArticulationManager.Entities.MidiEventData.Value;

namespace ArticulationManager.Entities.MidiEventData
{
    public interface IMidiControlChangeFactory
    {
        public MidiControlChange Create( int ccNumber, int ccValue );
        public MidiControlChange Create( int channel, int ccNumber, int ccValue );

        public class DefaultFactory : IMidiControlChangeFactory
        {
            public MidiControlChange Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public MidiControlChange Create( int channel, int ccNumber, int ccValue )
            {
                return new MidiControlChange(
                    new MidiChannel( channel ),
                    new MidiControlChangeNumber( ccNumber ),
                    new MidiControlChangeValue( ccValue )
                );
            }
        }
    }
}
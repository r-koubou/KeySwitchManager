using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IControlChangeFactory
    {
        public ControlChange Create( int ccNumber, int ccValue );
        public ControlChange Create( int channel, int ccNumber, int ccValue );

        public class DefaultFactory : IControlChangeFactory
        {
            public ControlChange Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public ControlChange Create( int channel, int ccNumber, int ccValue )
            {
                return new ControlChange( new Channel( channel ), new ControlChangeNumber( ccNumber ), new ControlChangeValue( ccValue ) );
            }
        }
    }
}
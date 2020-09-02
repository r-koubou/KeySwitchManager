using ArticulationManager.Domain.Commons;
using ArticulationManager.Domain.MidiMessages.Aggregate;
using ArticulationManager.Domain.MidiMessages.Value;

namespace ArticulationManager.Domain.MidiMessages
{
    public interface IControlChangeFactory
    {
        public ControlChange Create( ulong id, int ccNumber, int ccValue );
        public ControlChange Create( ulong id, int channel, int ccNumber, int ccValue );

        public class DefaultFactory : IControlChangeFactory
        {
            public ControlChange Create( ulong id, int ccNumber, int ccValue )
            {
                return Create( id, 0x00, ccNumber, ccValue );
            }

            public ControlChange Create( ulong id, int channel, int ccNumber, int ccValue )
            {
                return new ControlChange( new EntityId( id ), new Channel( channel ), new ControlChangeNumber( ccNumber ), new ControlChangeValue( ccValue ) );
            }
        }
    }
}
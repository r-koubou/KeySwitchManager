using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IControlChangeFactory : IMidiMessageFactory
    {
        public ControlChange Create( int ccNumber, int ccValue );
        public ControlChange Create( int channel, int ccNumber, int ccValue );

        public class Default : IControlChangeFactory
        {
            public IMessage Create( int status, int channel, int data1, int data2 )
            {
                return Create( channel, data1, data2 );
            }

            public ControlChange Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public ControlChange Create( int channel, int ccNumber, int ccValue )
            {
                return new ControlChange( new MidiChannel( channel ), new ControlChangeNumber( ccNumber ), new ControlChangeValue( ccValue ) );
            }
        }
    }
}
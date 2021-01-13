using KeySwitchManager.Domain.MidiMessages.Aggregate;
using KeySwitchManager.Domain.MidiMessages.Helpers;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiControlChangeFactory : IMidiMessageFactory<MidiControlChange>
    {
        public MidiControlChange Create( int ccNumber, int ccValue );

        public static IMidiControlChangeFactory Default => new DefaultFactory();

        public static MidiControlChange Zero =>
            new MidiControlChange(
                IMidiStatusFactory.Default.Create( 0 ),
                IMidiControlChangeNumberFactory.Default.Create( 0 ),
                IMidiControlChangeValueFactory.Default.Create( 0 )
            );

        private class DefaultFactory : IMidiControlChangeFactory
        {
            public MidiControlChange Create( int ccNumber, int ccValue )
            {
                return Create( MidiStatusHelper.ControlChange, ccNumber, ccValue );
            }

            public MidiControlChange Create( int status, int data1, int data2 )
            {
                return new MidiControlChange(
                    IMidiStatusFactory.Default.Create( status ),
                    IMidiControlChangeNumberFactory.Default.Create( data1 ),
                    IMidiControlChangeValueFactory.Default.Create( data2 )
                );
            }
        }
    }
}
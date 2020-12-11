using KeySwitchManager.Domain.MidiMessages.Services;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiStatusFactory
    {
        public MidiStatus Create( int status );
        public MidiStatus Create( int status, int channel );

        public static IMidiStatusFactory Default => new DefaultFactory();

        private class DefaultFactory : IMidiStatusFactory
        {
            public MidiStatus Create( int data )
            {
                return new MidiStatus( data );
            }

            public MidiStatus Create( int status, int channel )
            {
                return new MidiStatus( MidiStatusHelper.MakeStatus( status, channel ) );
            }
        }
    }
}
using KeySwitchManager.Common.Numbers;
using KeySwitchManager.Domain.MidiMessages.Helpers;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class MidiStatus : MidiMessageData
    {
        public MidiChannel Channel =>
            IMidiChannelFactory.Default.Create( MidiStatusHelper.GetChannel( Value ) );

        public MidiStatus( int value ) : base( value )
        {}

        public MidiStatus( int value, int channel ) : base( value | ( channel & 0x0F ) )
        {}
    }

    #region Factory
    public interface IMidiStatusFactory
    {
        public static IMidiStatusFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiStatus Create( int value );
        MidiStatus Create( int status, int channel );

        private class DefaultFactory : IMidiStatusFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0xFF;

            public MidiStatus Create( int value )
            {
                RangeValidateHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiStatus( value );
            }

            public MidiStatus Create( int status, int channel )
            {
                var value = ( status | channel ) & 0xF;
                RangeValidateHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiStatus( value );
            }

        }
    }
    #endregion Factory
}
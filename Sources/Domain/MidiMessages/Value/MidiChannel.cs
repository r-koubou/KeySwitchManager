using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiChannel : MidiMessageData
    {
        public MidiChannel( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiChannelFactory
    {
        public static IMidiChannelFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiChannel Create( int value );

        private class DefaultFactory : IMidiChannelFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x0F;

            public MidiChannel Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiChannel( value );
            }
        }
    }
    #endregion Factory
}
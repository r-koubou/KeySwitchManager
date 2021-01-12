using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiVelocity : MidiMessageData
    {
        public MidiVelocity( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiVelocityFactory
    {
        public static IMidiVelocityFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiVelocity Create( int value );

        private class DefaultFactory : IMidiVelocityFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiVelocity Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiVelocity( value );
            }
        }
    }
    #endregion Factory
}
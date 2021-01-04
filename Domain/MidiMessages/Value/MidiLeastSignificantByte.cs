using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiLeastSignificantByte : MidiMessageData
    {
        public MidiLeastSignificantByte( int value ) : base( value )
        {}
    }
    #region Factory
    public interface IMidiLeastSignificantByteFactory
    {
        public static IMidiLeastSignificantByteFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiLeastSignificantByte Create( int value );

        private class DefaultFactory : IMidiLeastSignificantByteFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiLeastSignificantByte Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiLeastSignificantByte( value );
            }
        }
    }
    #endregion Factory
}
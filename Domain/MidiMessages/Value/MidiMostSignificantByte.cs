using KeySwitchManager.Common.Numbers;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiMostSignificantByte : MidiMessageData
    {
        public MidiMostSignificantByte( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiMostSignificantByteFactory
    {
        public static IMidiMostSignificantByteFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiMostSignificantByte Create( int value );

        private class DefaultFactory : IMidiMostSignificantByteFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiMostSignificantByte Create( int value )
            {
                RangeValidateHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiMostSignificantByte( value );
            }
        }
    }
    #endregion Factory
}
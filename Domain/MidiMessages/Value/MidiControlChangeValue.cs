using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiControlChangeValue : MidiMessageData
    {
        public MidiControlChangeValue( int value ) : base( value )
        {}

        public override string ToString()
            => Value.ToString();
    }

    #region Factory
    public interface IMidiControlChangeValueFactory
    {
        public static IMidiControlChangeValueFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiControlChangeValue Create( int value );

        private class DefaultFactory : IMidiControlChangeValueFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiControlChangeValue Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiControlChangeValue( value );
            }
        }
    }
    #endregion Factory
}
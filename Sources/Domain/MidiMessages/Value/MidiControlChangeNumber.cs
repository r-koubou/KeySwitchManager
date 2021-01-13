using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiControlChangeNumber : MidiMessageData
    {
        public MidiControlChangeNumber( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiControlChangeNumberFactory
    {
        public static IMidiControlChangeNumberFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiControlChangeNumber Create( int value );

        private class DefaultFactory : IMidiControlChangeNumberFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiControlChangeNumber Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiControlChangeNumber( value );
            }
        }
    }
    #endregion Factory
}
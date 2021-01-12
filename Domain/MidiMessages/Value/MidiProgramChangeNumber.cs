using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiProgramChangeNumber : MidiMessageData
    {
        public MidiProgramChangeNumber( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiProgramChangeNumberFactory
    {
        public static IMidiProgramChangeNumberFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiProgramChangeNumber Create( int value );

        private class DefaultFactory : IMidiProgramChangeNumberFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiProgramChangeNumber Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiProgramChangeNumber( value );
            }
        }
    }
    #endregion Factory
}
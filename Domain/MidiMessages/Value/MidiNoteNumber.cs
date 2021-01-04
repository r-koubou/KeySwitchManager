using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    public class MidiNoteNumber : MidiMessageData
    {
        public MidiNoteNumber( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IMidiNoteNumberFactory
    {
        public static IMidiNoteNumberFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        MidiNoteNumber Create( int value );

        private class DefaultFactory : IMidiNoteNumberFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0x7F;

            public MidiNoteNumber Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new MidiNoteNumber( value );
            }
        }
    }
    #endregion Factory
}
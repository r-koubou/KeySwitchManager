using RkHelper.Number;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class GenericMidiData : MidiMessageData
    {
        public static readonly GenericMidiData Empty = new GenericMidiData( 0x00 );

        public GenericMidiData( int value ) : base( value )
        {}
    }

    #region Factory
    public interface IGenericMidiDataFactory
    {
        public static IGenericMidiDataFactory Default => new DefaultFactory();

        int MinValue { get; }
        int MaxValue { get; }

        GenericMidiData Create( int value );

        private class DefaultFactory : IGenericMidiDataFactory
        {
            public int MinValue => 0x00;
            public int MaxValue => 0xFF;

            public GenericMidiData Create( int value )
            {
                NumberHelper.ValidateRange( value, MinValue, MaxValue );
                return new GenericMidiData( value );
            }
        }
    }
    #endregion Factory
}
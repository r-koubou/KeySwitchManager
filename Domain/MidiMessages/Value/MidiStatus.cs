using KeySwitchManager.Domain.MidiMessages.Services;

namespace KeySwitchManager.Domain.MidiMessages.Value
{
    /// <summary>
    /// Generic MIDI message data.
    /// </summary>
    public class MidiStatus : MidiMessageData
    {
        public const int MinValue = 0x00;
        public const int MaxValue = 0xFF;

        public MidiChannel Channel =>
            new MidiChannel( MidiStatusHelper.GetChannel( Value ) );

        public MidiStatus( int value )
            : base( value, MinValue, MaxValue )
        {}

        public MidiStatus( int value, int channel )
            : base( value | ( channel & 0x0F ), MinValue, MaxValue )
        {}
    }
}
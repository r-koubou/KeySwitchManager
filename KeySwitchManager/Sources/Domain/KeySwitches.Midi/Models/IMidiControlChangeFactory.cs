using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Helpers;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models
{
    public interface IMidiControlChangeFactory : IMidiMessageFactory<MidiControlChange>
    {
        public MidiControlChange Create( int ccNumber, int ccValue );

        public static IMidiControlChangeFactory Default => new DefaultFactory();

        public static MidiControlChange Zero =>
            new MidiControlChange(
                new MidiStatus( 0 ),
                new MidiControlChangeNumber( 0 ),
                new MidiControlChangeValue( 0 )
            );

        private class DefaultFactory : IMidiControlChangeFactory
        {
            public MidiControlChange Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public MidiControlChange Create( int channel, int data1, int data2 )
            {
                return new MidiControlChange(
                    new MidiStatus( MidiStatusHelper.MakeStatus( MidiStatusHelper.ControlChange, channel ) ),
                    new MidiControlChangeNumber( data1 ),
                    new MidiControlChangeValue( data2 )
                );
            }
        }
    }
}
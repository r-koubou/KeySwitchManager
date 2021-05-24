using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Helpers;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models
{
    public interface IMidiProgramChangeFactory : IMidiMessageFactory<MidiProgramChange>
    {
        public MidiProgramChange Create( int channel, int pcNumber );
        public MidiProgramChange Create( int pcNumber );

        public static IMidiProgramChangeFactory Default => new DefaultFactory();

        public static MidiProgramChange Zero =>
            new MidiProgramChange(
                new MidiStatus( MidiStatusHelper.ProgramChange ),
                new MidiProgramChangeNumber( 0 )
            );

        private class DefaultFactory : IMidiProgramChangeFactory
        {
            public MidiProgramChange Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public MidiProgramChange Create( int channel, int pcNumber )
            {
                return Create(
                    MidiStatusHelper.MakeStatus( MidiStatusHelper.ProgramChange, channel ),
                    pcNumber,
                    0x00
                );
            }

            public MidiProgramChange Create( int channel, int data1, int data2 )
            {
                return new MidiProgramChange(
                    new MidiStatus( MidiStatusHelper.MakeStatus( MidiStatusHelper.ProgramChange, channel ) ),
                    new MidiProgramChangeNumber( data1 )
                );
            }
        }
    }
}
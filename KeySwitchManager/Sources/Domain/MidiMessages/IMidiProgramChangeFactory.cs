using KeySwitchManager.Domain.MidiMessages.Entity;
using KeySwitchManager.Domain.MidiMessages.Helper;
using KeySwitchManager.Domain.MidiMessages.Value;

namespace KeySwitchManager.Domain.MidiMessages
{
    public interface IMidiProgramChangeFactory : IMidiMessageFactory<MidiProgramChange>
    {
        public MidiProgramChange Create( int channel, int pcNumber );

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
                return Create( MidiStatusHelper.ProgramChange, pcNumber );
            }

            public MidiProgramChange Create( int channel, int pcNumber )
            {
                return Create(
                    MidiStatusHelper.MakeStatus( MidiStatusHelper.ProgramChange, channel ),
                    pcNumber,
                    0x00
                );
            }

            public MidiProgramChange Create( int status, int data1, int data2 )
            {
                return new MidiProgramChange(
                    new MidiStatus( status ),
                    new MidiProgramChangeNumber( data1 )
                );
            }
        }
    }
}
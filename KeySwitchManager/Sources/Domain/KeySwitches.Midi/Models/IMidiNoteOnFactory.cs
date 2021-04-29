using KeySwitchManager.Domain.KeySwitches.Midi.Models.Entities;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Helpers;
using KeySwitchManager.Domain.KeySwitches.Midi.Models.Values;

namespace KeySwitchManager.Domain.KeySwitches.Midi.Models
{
    public interface IMidiNoteOnFactory : IMidiMessageFactory<MidiNoteOn>
    {
        public MidiNoteOn Create( int noteNumber, int velocity );

        public static IMidiNoteOnFactory Default => new DefaultFactory();

        public static MidiNoteOn Zero =>
            new MidiNoteOn(
                new MidiStatus( MidiStatusHelper.NoteOn ),
                new MidiNoteNumber( 0 ),
                new MidiVelocity( 0 )
            );

        private class DefaultFactory : IMidiNoteOnFactory
        {
            public MidiNoteOn Create( int noteNumber, int velocity )
            {
                return  Create( MidiStatusHelper.NoteOn, noteNumber, velocity );
            }

            public MidiNoteOn Create( int status, int data1, int data2 )
            {
                return new MidiNoteOn(
                    new MidiStatus( status ),
                    new MidiNoteNumber( data1 ),
                    new MidiVelocity( data2 )
                );
            }
        }
    }
}
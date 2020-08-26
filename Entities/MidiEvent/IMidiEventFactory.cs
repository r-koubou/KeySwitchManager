using ArticulationManager.Entities.MidiEvent.Aggregate;
using ArticulationManager.Entities.MidiEvent.Value;

namespace ArticulationManager.Entities.MidiEvent
{
    public interface IMidiEventFactory
    {
        public IMidiEvent Create( int status, int data1, int data2 );

        public IMidiEvent CreateNoteOn( int noteNumber, int velocity );
        public IMidiEvent CreateControlChange( int ccNumber, int ccValue );
        public IMidiEvent CreateProgramChange( int pcNumber );

        public IMidiEvent CreateNoteOn( int channel, int noteNumber, int velocity );
        public IMidiEvent CreateControlChange( int channel, int ccNumber, int ccValue );
        public IMidiEvent CreateProgramChange( int channel, int pcNumber );

    }

    public class SimpleMidiEventFactory : IMidiEventFactory
    {
        public IMidiEvent Create( int status, int data1, int data2 )
        {
            return new GenericMidiEvent(
                new MidiStatusCode( status ),
                new GenericMidiEventValue( data1 ),
                new GenericMidiEventValue( data2 )
            );
        }

        public IMidiEvent CreateNoteOn( int noteNumber, int velocity )
        {
            return  CreateNoteOn( 0x00, noteNumber, velocity );
        }

        public IMidiEvent CreateControlChange( int ccNumber, int ccValue )
        {
            return CreateControlChange( 0x00, ccNumber, ccValue );
        }

        public IMidiEvent CreateProgramChange( int pcNumber )
        {
            return CreateProgramChange( 0x00, pcNumber );
        }

        public IMidiEvent CreateNoteOn( int channel, int noteNumber, int velocity )
        {
            return new MidiNoteOn(
                new MidiChannel( channel ),
                new MidiNoteNumber( noteNumber ),
                new MidiVelocity( velocity )
            );
        }

        public IMidiEvent CreateControlChange( int channel, int ccNumber, int ccValue )
        {
            return new MidiControlChange(
                new MidiChannel( channel ),
                new MidiControlChangeNumber( ccNumber ),
                new MidiControlChangeValue( ccValue )
            );
        }

        public IMidiEvent CreateProgramChange( int channel, int pcNumber )
        {
            return new MidiProgramChange(
                new MidiChannel( channel ),
                new MidiProgramChangeNumber( pcNumber )
            );
        }
    }
}
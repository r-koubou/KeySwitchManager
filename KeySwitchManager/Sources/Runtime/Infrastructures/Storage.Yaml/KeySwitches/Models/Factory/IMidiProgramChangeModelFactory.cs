using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Factory
{
    public interface IMidiProgramChangeModelFactory : IMidiChannelVoiceMessageModelFactory<MidiProgramChangeModel>
    {
        public MidiProgramChangeModel Create( int pcNumber );
        public MidiProgramChangeModel Create( int channel, int pcNumber );

        public static IMidiProgramChangeModelFactory Default => new DefaultModelFactory();

        private class DefaultModelFactory : IMidiProgramChangeModelFactory
        {
            public MidiProgramChangeModel Create( int pcNumber )
            {
                return Create( 0x00, pcNumber );
            }

            public MidiProgramChangeModel Create( int channel, int pcNumber )
            {
                return Create( channel, pcNumber, 0x00 );
            }

            public MidiProgramChangeModel Create( int channel, int data1, int data2 )
            {
                return new MidiProgramChangeModel( channel, data1 );
            }
        }
    }
}
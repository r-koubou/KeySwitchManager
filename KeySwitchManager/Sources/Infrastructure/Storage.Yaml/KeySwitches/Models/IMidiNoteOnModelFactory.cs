using KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities;

namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models
{
    public interface IMidiNoteOnModelFactory : IMidiChannelVoiceMessageModelFactory<MidiNoteOnModel>
    {
        public MidiNoteOnModel Create( int noteNumber, int velocity );

        public static IMidiNoteOnModelFactory Default => new DefaultModelFactory();

        private class DefaultModelFactory : IMidiNoteOnModelFactory
        {
            public MidiNoteOnModel Create( int noteNumber, int velocity )
            {
                return  Create( 0x00, noteNumber, velocity );
            }

            public MidiNoteOnModel Create( int channel, int data1, int data2 )
            {
                return new MidiNoteOnModel(
                    channel,
                    data1,
                    data2
                );
            }
        }
    }
}
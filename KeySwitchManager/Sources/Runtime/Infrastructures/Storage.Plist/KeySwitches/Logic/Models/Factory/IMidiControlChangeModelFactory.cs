using KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations;

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Factory
{
    public interface IMidiControlChangeModelFactory : IMidiChannelVoiceMessageModelFactory<MidiControlChangeModel>
    {
        public MidiControlChangeModel Create( int ccNumber, int ccValue );

        public static IMidiControlChangeModelFactory Default => new DefaultModelFactory();

        private class DefaultModelFactory : IMidiControlChangeModelFactory
        {
            public MidiControlChangeModel Create( int ccNumber, int ccValue )
            {
                return Create( 0x00, ccNumber, ccValue );
            }

            public MidiControlChangeModel Create( int channel, int data1, int data2 )
            {
                return new MidiControlChangeModel(
                    channel,
                    data1,
                    data2
                );
            }
        }
    }
}
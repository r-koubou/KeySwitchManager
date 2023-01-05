namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public class MidiNoteOnModel : IMidiChannelVoiceMessageModel
    {
        public int Channel { get; set; }

        // Note
        public int Data1 { get; set; }

        // Velocity
        public int Data2 { get; set; }

        public MidiNoteOnModel() {}

        public MidiNoteOnModel( int channel, int data1, int data2 )
        {
            Channel = channel;
            Data1   = data1;
            Data2   = data2;
        }
    }
}

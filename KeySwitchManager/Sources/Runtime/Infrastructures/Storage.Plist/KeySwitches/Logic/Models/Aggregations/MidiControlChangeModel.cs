namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public class MidiControlChangeModel : IMidiChannelVoiceMessageModel
    {
        public int Channel { get; set; }

        // ControlNumber
        public int Data1 { get; set; }

        // Data
        public int Data2 { get; set; }

        public MidiControlChangeModel() {}

        public MidiControlChangeModel( int channel, int data1, int data2 )
        {
            Channel = channel;
            Data1   = data1;
            Data2   = data2;
        }
    }
}

namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public class MidiProgramChangeModel : IMidiChannelVoiceMessageModel
    {
        public int Channel { get; set; }

        // ProgramNumber
        public int Data1 { get; set; }

        public int Data2 { get; set; } = 0x00;

        public MidiProgramChangeModel() {}

        public MidiProgramChangeModel( int channel, int data )
        {
            Channel = channel;
            Data1   = data;
            Data2   = 0x00;
        }
    }
}

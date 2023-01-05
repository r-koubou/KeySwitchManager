namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public interface IMidiChannelVoiceMessageModel : IMidiMessageModel
    {
        public int Channel { get; set; }
    }
}
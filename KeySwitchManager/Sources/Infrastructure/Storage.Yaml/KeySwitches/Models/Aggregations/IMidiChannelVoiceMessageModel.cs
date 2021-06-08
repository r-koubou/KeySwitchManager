namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public interface IMidiChannelVoiceMessageModel : IMidiMessageModel
    {
        public int Channel { get; set; }
    }
}
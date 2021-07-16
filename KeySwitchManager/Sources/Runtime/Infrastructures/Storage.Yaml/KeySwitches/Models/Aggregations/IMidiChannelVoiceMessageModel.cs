namespace KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public interface IMidiChannelVoiceMessageModel : IMidiMessageModel
    {
        public int Channel { get; set; }
    }
}
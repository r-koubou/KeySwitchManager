namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Entities
{
    public interface IMidiChannelVoiceMessageModel : IMidiMessageModel
    {
        public int Channel { get; set; }
    }
}
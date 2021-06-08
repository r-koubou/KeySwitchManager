namespace KeySwitchManager.Storage.Yaml.KeySwitches.Models.Aggregations
{
    public interface IMidiMessageModel
    {
        public int Status { get; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
    }
}
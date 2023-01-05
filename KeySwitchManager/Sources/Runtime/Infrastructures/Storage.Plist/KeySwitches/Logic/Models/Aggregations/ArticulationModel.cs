namespace KeySwitchManager.Infrastructures.Storage.Plist.KeySwitches.Logic.Models.Aggregations
{
    public class ArticulationModel
    {
        public int Id { get; set; }
        public int ArticulationId { get; set; }
        public string Name { get; set; } = string.Empty;
        public OutputModel Output { get; set; } = new();
    }
}

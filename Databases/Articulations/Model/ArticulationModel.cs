namespace ArticulationManager.Databases.Articulations.Model
{
    public class ArticulationModel
    {
        public ulong Id { get; set; }
        public string Guid { get; set; } = System.Guid.NewGuid().ToString();
        public string DeveloperName { get; set; } = "Unknown";
        public string ProductName { get; set; } = "Unknown";
        public string ArticulationName { get; set; } = "Unknown";
        public string ArticulationType { get; set; } = Domain.Articulations.Value.ArticulationType.Default.ToString();
        public int ArticulationGroup { get; set; }
        public int ArticulationColor { get; set; }

        public ArticulationModel()
        {}

        public ArticulationModel(
            string developerName,
            string productName,
            string articulationName,
            int articulationGroup,
            int articulationColor )
        {
            DeveloperName     = developerName;
            ProductName       = productName;
            ArticulationName  = articulationName;
            ArticulationGroup = articulationGroup;
            ArticulationColor = articulationColor;
        }
    }
}
namespace ArticulationManager.UseCases.KeySwitches.Exporting.Text
{
    public class InputData
    {
        public string DeveloperName { get; }
        public string ProductName { get; }

        public InputData(
            string developerName,
            string productName )
        {
            DeveloperName      = developerName;
            ProductName        = productName;
        }
    }
}
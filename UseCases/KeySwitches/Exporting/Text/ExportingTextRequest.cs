namespace KeySwitchManager.UseCases.KeySwitches.Exporting.Text
{
    public class ExportingTextRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }

        public ExportingTextRequest(
            string developerName,
            string productName )
        {
            DeveloperName      = developerName;
            ProductName        = productName;
        }
    }
}
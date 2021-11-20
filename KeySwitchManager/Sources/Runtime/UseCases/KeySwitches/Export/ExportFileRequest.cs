namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public class ExportFileRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public ExportFileRequest(
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }
}
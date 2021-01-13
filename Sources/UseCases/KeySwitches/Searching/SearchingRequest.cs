namespace KeySwitchManager.UseCases.KeySwitches.Searching
{
    public class SearchingRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public SearchingRequest(
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
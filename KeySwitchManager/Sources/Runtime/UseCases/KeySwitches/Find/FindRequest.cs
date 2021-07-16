namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public class FindRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public FindRequest(
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
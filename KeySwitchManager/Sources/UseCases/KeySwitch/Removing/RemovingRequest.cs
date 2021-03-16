namespace KeySwitchManager.UseCases.KeySwitch.Removing
{
    public class RemovingRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public RemovingRequest( string developerName, string productName, string instrumentName )
        {
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }
}
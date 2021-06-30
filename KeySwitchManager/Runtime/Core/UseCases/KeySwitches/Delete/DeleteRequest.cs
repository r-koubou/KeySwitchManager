namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public class DeleteRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public DeleteRequest( string developerName, string productName, string instrumentName )
        {
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }
}
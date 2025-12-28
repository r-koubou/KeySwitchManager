namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public class DeleteInputValue
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public DeleteInputValue( string developerName, string productName, string instrumentName )
        {
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }
}

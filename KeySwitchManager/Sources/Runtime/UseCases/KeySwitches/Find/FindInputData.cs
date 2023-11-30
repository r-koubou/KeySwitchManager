using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public sealed class FindInputValue
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public FindInputValue(
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }

    public sealed class FindInputData : InputData<FindInputValue>
    {
        public FindInputData( FindInputValue value ) : base( value ) {}
    }
}

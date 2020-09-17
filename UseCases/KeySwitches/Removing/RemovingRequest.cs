using KeySwitchManager.Common.Utilities;

namespace KeySwitchManager.UseCases.KeySwitches.Removing
{
    public class RemovingRequest
    {
        public string DeveloperName { get; }
        public string ProductName { get; }

        public RemovingRequest( string developerName, string productName )
        {
            StringHelper.ValidateNullOrTrimEmpty( developerName );
            StringHelper.ValidateNullOrTrimEmpty( productName );
            DeveloperName = developerName;
            ProductName   = productName;
        }
    }
}
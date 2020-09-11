using ArticulationManager.Common.Utilities;

namespace ArticulationManager.UseCases.Articulations.Removing
{
    public class InputData
    {
        public string DeveloperName { get; }
        public string ProductName { get; }

        public InputData( string developerName, string productName )
        {
            StringHelper.ValidateNullOrTrimEmpty( developerName );
            StringHelper.ValidateNullOrTrimEmpty( productName );
            DeveloperName = developerName;
            ProductName   = productName;
        }
    }
}
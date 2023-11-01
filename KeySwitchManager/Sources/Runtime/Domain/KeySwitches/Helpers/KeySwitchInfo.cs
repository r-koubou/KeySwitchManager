using RkHelper.Primitives;

namespace KeySwitchManager.Domain.KeySwitches.Helpers
{
    public class KeySwitchInfo
    {
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string Author { get; }
        public string Description { get; }

        public KeySwitchInfo(
            string developerName,
            string productName,
            string author = "",
            string description = "" )
        {
            StringHelper.ValidateEmpty( developerName );
            StringHelper.ValidateNull( productName );
            StringHelper.ValidateNull( author );
            StringHelper.ValidateNull( description );

            DeveloperName = developerName;
            ProductName   = productName;
            Author        = author;
            Description   = description;
        }
    }
}

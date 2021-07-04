using System;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public class ExportDawRequest
    {
        public Guid Guid { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public ExportDawRequest(
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            Guid           = default;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }

        public ExportDawRequest(
            Guid guid = default,
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            Guid           = guid;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }
    }
}
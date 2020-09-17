using System;

namespace KeySwitchManager.UseCases.KeySwitches.Exporting.Text
{
    public class ExportingTextRequest
    {
        public Guid Guid { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public ExportingTextRequest(
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            Guid           = default;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }

        public ExportingTextRequest(
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
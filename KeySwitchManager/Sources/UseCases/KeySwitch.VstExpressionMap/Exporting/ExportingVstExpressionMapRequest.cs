using System;

namespace KeySwitchManager.UseCases.KeySwitch.VstExpressionMap.Exporting
{
    public class ExportingVstExpressionMapRequest
    {
        public Guid Guid { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }
        public string InstrumentName { get; }

        public ExportingVstExpressionMapRequest(
            string developerName = "",
            string productName = "",
            string instrumentName = "" )
        {
            Guid           = default;
            DeveloperName  = developerName;
            ProductName    = productName;
            InstrumentName = instrumentName;
        }

        public ExportingVstExpressionMapRequest(
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
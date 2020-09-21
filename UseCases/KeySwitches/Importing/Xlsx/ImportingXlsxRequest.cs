using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxRequest
    {
        public FilePath FilePath { get; }
        public string DeveloperName { get; }
        public string ProductName { get; }

        public ImportingXlsxRequest(
            FilePath filePath,
            string developerName,
            string productName )
        {
            FilePath      = filePath;
            DeveloperName = developerName;
            ProductName   = productName;
        }
    }
}
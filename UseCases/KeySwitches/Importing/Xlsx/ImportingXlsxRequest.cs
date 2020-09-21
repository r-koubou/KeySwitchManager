using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxRequest
    {
        public FilePath FilePath { get; }
        public ImportingXlsxRequest( FilePath filePath )
        {
            FilePath      = filePath;
        }
    }
}
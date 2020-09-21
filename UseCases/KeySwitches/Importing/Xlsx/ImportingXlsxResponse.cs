namespace KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx
{
    public class ImportingXlsxResponse
    {
        public int UpdatedCount { get; }

        public ImportingXlsxResponse( int updatedCount )
        {
            UpdatedCount = updatedCount;
        }
    }
}
namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public class ImportFileResponse
    {
        public int InsertedCount { get; }
        public int UpdatedCount { get; }

        public ImportFileResponse( int insertedCount, int updatedCount )
        {
            InsertedCount = insertedCount;
            UpdatedCount  = updatedCount;
        }
    }
}

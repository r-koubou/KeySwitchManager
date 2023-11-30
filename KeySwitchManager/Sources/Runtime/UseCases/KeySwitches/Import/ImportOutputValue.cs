namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public sealed class ImportOutputValue
    {
        public int InsertedCount { get; }
        public int UpdatedCount { get; }

        public ImportOutputValue( int insertedCount, int updatedCount )
        {
            InsertedCount = insertedCount;
            UpdatedCount  = updatedCount;
        }
    }
}

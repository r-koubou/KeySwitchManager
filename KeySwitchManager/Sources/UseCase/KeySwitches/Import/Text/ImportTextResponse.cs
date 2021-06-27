namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public class ImportTextResponse
    {
        public int UpdatedCount { get; }

        public ImportTextResponse( int updatedCount )
        {
            UpdatedCount = updatedCount;
        }

    }
}
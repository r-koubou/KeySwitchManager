namespace KeySwitchManager.UseCases.KeySwitches.Importing.Text
{
    public class ImportingTextResponse
    {
        public int UpdatedCount { get; }

        public ImportingTextResponse( int updatedCount )
        {
            UpdatedCount = updatedCount;
        }

    }
}
namespace KeySwitchManager.UseCases.KeySwitch.Importing.Text
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
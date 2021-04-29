namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public class TextImportResponse
    {
        public int UpdatedCount { get; }

        public TextImportResponse( int updatedCount )
        {
            UpdatedCount = updatedCount;
        }

    }
}
namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public class SpreadsheetExportResponse
    {
        public bool Result { get; }

        public SpreadsheetExportResponse( bool result )
        {
            Result = result;
        }
    }
}
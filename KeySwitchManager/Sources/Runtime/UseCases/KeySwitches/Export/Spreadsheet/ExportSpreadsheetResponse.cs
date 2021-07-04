namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public class ExportSpreadsheetResponse
    {
        public bool Result { get; }

        public ExportSpreadsheetResponse( bool result )
        {
            Result = result;
        }
    }
}
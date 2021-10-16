namespace KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet
{
    public class CreateSpreadsheetTemplateResponse
    {
        public bool Result { get; }

        public CreateSpreadsheetTemplateResponse( bool result )
        {
            Result = result;
        }
    }
}
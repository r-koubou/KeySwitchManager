namespace KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate
{
    public class SpreadsheetTemplateExportResponse
    {
        public bool Result { get; }

        public SpreadsheetTemplateExportResponse( bool result )
        {
            Result = result;
        }
    }
}
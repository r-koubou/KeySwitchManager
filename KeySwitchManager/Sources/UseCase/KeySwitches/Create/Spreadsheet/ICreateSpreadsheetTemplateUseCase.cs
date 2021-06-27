namespace KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet
{
    public interface ICreateSpreadsheetTemplateUseCase
    {
        CreateSpreadsheetTemplateResponse Execute( CreateSpreadsheetTemplateRequest request );
    }
}
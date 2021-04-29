namespace KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate
{
    public interface ISpreadsheetTemplateExportUseCase
    {
        SpreadsheetTemplateExportResponse Execute( SpreadsheetTemplateExportRequest request );
    }
}
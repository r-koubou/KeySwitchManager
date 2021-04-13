namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public interface ISpreadsheetExportUseCase
    {
        SpreadsheetExportResponse Execute( SpreadsheetExportRequest request );
    }
}
namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public interface IExportSpreadsheetUseCase
    {
        ExportSpreadsheetResponse Execute( ExportSpreadsheetRequest request );
    }
}
namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public interface ISpreadSheetImportUseCase
    {
        public SpreadsheetImportResponse Execute( SpreadsheetImportRequest request );
    }
}
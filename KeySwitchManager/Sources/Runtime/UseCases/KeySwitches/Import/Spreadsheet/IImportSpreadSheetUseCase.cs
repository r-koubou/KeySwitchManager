namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public interface IImportSpreadSheetUseCase
    {
        public ImportSpreadSheetResponse Execute( ImportSpreadSheetRequest request );
    }
}
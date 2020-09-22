namespace KeySwitchManager.UseCases.KeySwitches.Importing.Xlsx
{
    public interface IImportingXlsxUseCase
    {
        public ImportingXlsxResponse Execute( ImportingXlsxRequest request );
    }
}
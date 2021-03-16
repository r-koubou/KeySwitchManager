namespace KeySwitchManager.UseCases.KeySwitch.Importing.Xlsx
{
    public interface IImportingXlsxUseCase
    {
        public ImportingXlsxResponse Execute( ImportingXlsxRequest request );
    }
}
namespace KeySwitchManager.UseCases.KeySwitches.Exporting
{
    public interface IExportingXlsxUseCase
    {
        ExportingXlsxResponse Execute( ExportingXlsxRequest request );
    }
}
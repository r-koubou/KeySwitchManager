namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public interface IExportingXlsxUseCase
    {
        ExportingXlsxResponse Execute( ExportingXlsxRequest request );
    }
}
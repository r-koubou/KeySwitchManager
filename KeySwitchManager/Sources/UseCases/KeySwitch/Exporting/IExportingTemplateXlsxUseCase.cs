namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public interface IExportingTemplateXlsxUseCase
    {
        ExportingTemplateXlsxResponse Execute( ExportingTemplateXlsxRequest request );
    }
}
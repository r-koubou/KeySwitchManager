namespace KeySwitchManager.UseCases.KeySwitches.Exporting
{
    public interface IExportingTemplateXlsxUseCase
    {
        ExportingTemplateXlsxResponse Execute( ExportingTemplateXlsxRequest request );
    }
}
namespace KeySwitchManager.UseCases.KeySwitches.Exporting
{
    public interface IExportingTextUseCase
    {
        public ExportingTextResponse Execute( ExportingTextRequest request );
    }
}
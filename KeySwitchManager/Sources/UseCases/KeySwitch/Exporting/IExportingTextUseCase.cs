namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public interface IExportingTextUseCase
    {
        public ExportingTextResponse Execute( ExportingTextRequest request );
    }
}
namespace KeySwitchManager.UseCases.KeySwitches.Exporting.Text
{
    public interface IExportingTextUseCase
    {
        public ExportingTextResponse Execute( ExportingTextRequest request );
    }
}
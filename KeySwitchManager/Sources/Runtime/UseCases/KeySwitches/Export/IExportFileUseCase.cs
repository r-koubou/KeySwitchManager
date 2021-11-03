namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportFileUseCase
    {
        public ExportFileResponse Execute( ExportFileRequest request );
    }
}
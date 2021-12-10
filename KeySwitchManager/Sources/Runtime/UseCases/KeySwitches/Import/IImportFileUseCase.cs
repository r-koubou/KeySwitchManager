namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportFileUseCase
    {
        public ImportFileResponse Execute( ImportFileRequest request );
    }
}
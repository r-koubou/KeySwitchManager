namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateFileUseCase
    {
        public CreateFileResponse Execute( CreateFileRequest request );
    }
}
namespace KeySwitchManager.UseCase.KeySwitches.Dump
{
    public interface IDumpFileUseCase
    {
        public DumpFileResponse Execute( DumpFileRequest request );
    }
}
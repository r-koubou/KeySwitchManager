using KeySwitchManager.UseCase.KeySwitches.Dump;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IDumpFileUseCase
    {
        public DumpFileResponse Execute( DumpFileRequest request );
    }
}
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Dump
{
    public interface IDumpFileUseCase
    {
        public DumpFileResponse Execute( DumpFileRequest request )
            => ExecuteAsync( request ).GetAwaiter().GetResult();

        public Task<DumpFileResponse> ExecuteAsync( DumpFileRequest request );
    }
}

using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateFileUseCase
    {
        public CreateFileResponse Execute( CreateFileRequest request )
            => ExecuteAsync( request ).GetAwaiter().GetResult();

        public Task<CreateFileResponse> ExecuteAsync( CreateFileRequest request );
    }
}

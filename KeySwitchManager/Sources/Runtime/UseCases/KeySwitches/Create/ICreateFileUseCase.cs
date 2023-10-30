using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateFileUseCase
    {
        public CreateFileResponse Execute()
            => ExecuteAsync( default ).GetAwaiter().GetResult();

        public Task<CreateFileResponse> ExecuteAsync( CancellationToken cancellationToken );
    }
}

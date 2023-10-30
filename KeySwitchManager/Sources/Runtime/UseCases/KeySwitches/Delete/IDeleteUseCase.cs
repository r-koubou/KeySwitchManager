using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public interface IDeleteUseCase
    {
        public DeleteResponse Execute( DeleteRequest request )
            => ExecuteAsync( request, default ).GetAwaiter().GetResult();

        public Task<DeleteResponse> ExecuteAsync( DeleteRequest request, CancellationToken cancellationToken );
    }
}

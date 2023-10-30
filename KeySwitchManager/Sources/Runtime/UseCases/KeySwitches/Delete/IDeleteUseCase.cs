using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public interface IDeleteUseCase
    {
        public DeleteResponse Execute( DeleteRequest request )
            => ExecuteAsync( request ).GetAwaiter().GetResult();

        public Task<DeleteResponse> ExecuteAsync( DeleteRequest request, CancellationToken cancellationToken = default );
    }
}

using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public interface IFindUseCase
    {
        public FindResponse Execute( FindRequest request )
            => ExecuteAsync( request ).GetAwaiter().GetResult();

        public Task<FindResponse> ExecuteAsync( FindRequest request, CancellationToken cancellationToken = default );
    }
}

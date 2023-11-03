using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateUseCase
    {
        public CreateResponse Execute()
            => ExecuteAsync().GetAwaiter().GetResult();

        public Task<CreateResponse> ExecuteAsync( CancellationToken cancellationToken = default );
    }
}

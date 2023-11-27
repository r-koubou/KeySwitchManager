using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Boundaries
{
    public interface IOutputPort<in TResponse>
    {
        void Handle( TResponse response )
            => HandleAsync( response ).GetAwaiter().GetResult();

        Task HandleAsync( TResponse response, CancellationToken cancellationToken = default );
    }
}

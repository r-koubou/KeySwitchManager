using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.Boundaries
{
    public interface IInputPort<in TRequest>
    {
        void Handle( TRequest request )
            => HandleAsync( request ).GetAwaiter().GetResult();

        Task HandleAsync( TRequest request, CancellationToken cancellationToken = default );
    }
}

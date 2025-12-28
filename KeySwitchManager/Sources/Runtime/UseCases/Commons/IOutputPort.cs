using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IOutputPort<in TOutputData>
    {
        void Handle( TOutputData outputData )
            => HandleAsync( outputData ).GetAwaiter().GetResult();

        Task HandleAsync( TOutputData outputData, CancellationToken cancellationToken = default );
    }
}

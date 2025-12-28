using System.Threading;
using System.Threading.Tasks;

namespace KeySwitchManager.UseCase.Commons
{
    public interface IInputPort<in TInputData>
    {
        void Handle( TInputData inputData )
            => HandleAsync( inputData ).GetAwaiter().GetResult();

        Task HandleAsync( TInputData inputData, CancellationToken cancellationToken = default );
    }
}

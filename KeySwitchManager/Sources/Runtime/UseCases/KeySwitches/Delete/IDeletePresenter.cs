using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public interface IDeletePresenter : IOutputPort<DeleteOutputData>
    {
        public void HandleDeleteBegin( DeleteInputData inputData )
            => HandleDeleteBeginAsync( inputData ).GetAwaiter().GetResult();

        public Task HandleDeleteBeginAsync( DeleteInputData inputData, CancellationToken cancellationToken = default );
    }
}

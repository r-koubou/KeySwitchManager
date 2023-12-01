using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.Presenters.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Delete;
using KeySwitchManager.Views.LogView;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public sealed class DeleteController
    {
        public void Execute( IKeySwitchRepository repository, string developer, string product, string instrument, ILogTextView logTextView )
            => ExecuteAsync( repository, developer, product, instrument, logTextView ).GetAwaiter().GetResult();

        public async Task ExecuteAsync( IKeySwitchRepository repository, string developer, string product, string instrument, ILogTextView logTextView, CancellationToken cancellationToken = default )
        {
            var presenter = new DeletePresenter( logTextView );
            var interactor = new DeleteInteractor( repository, presenter );
            var inputData = new DeleteInputData( new DeleteInputValue( developer, product, instrument ) );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}

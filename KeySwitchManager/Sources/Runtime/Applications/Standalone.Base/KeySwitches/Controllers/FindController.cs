using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Find;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers
{
    public sealed class FindController
    {
        public void Execute(
            string developerName,
            string productName,
            string instrumentName,
            IKeySwitchRepository repository,
            IFindPresenter presenter )
            => ExecuteAsync( developerName, instrumentName, productName, repository, presenter, CancellationToken.None ).GetAwaiter().GetResult();

        public async Task ExecuteAsync(
            string developerName,
            string instrumentName,
            string productName,
            IKeySwitchRepository repository,
            IFindPresenter presenter,
            CancellationToken cancellationToken = default )
        {
            IFindUseCase interactor = new FindInteractor( repository, presenter );
            var inputValue = new FindInputValue( developerName, productName, instrumentName );
            var inputData = new FindInputData( inputValue );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}

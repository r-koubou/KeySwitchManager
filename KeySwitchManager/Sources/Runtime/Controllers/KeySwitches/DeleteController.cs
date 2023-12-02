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
        public void Delete(
            IKeySwitchRepository repository,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter )
            => DeleteAsync( repository, developer, product, instrument, presenter ).GetAwaiter().GetResult();

        public async Task DeleteAsync(
            IKeySwitchRepository repository,
            string developer,
            string product,
            string instrument,
            IDeletePresenter presenter,
            CancellationToken cancellationToken = default )
        {
            var interactor = new DeleteInteractor( repository, presenter );
            var inputData = new DeleteInputData( new DeleteInputValue( developer, product, instrument ) );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}

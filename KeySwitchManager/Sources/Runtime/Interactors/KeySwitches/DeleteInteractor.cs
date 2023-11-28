using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Delete;

using RkHelper.Primitives;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class DeleteInteractor : IDeleteUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IDeletePresenter Presenter { get; }

        public DeleteInteractor(
            IKeySwitchRepository repository,
            IDeletePresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public async Task HandleAsync( DeleteInputData inputData, CancellationToken cancellationToken = default )
        {
            var developerName = inputData.Value.DeveloperName;
            var productName = inputData.Value.ProductName;
            var instrumentName = inputData.Value.InstrumentName;

            var removedCount = 0;

            await Presenter.HandleDeleteBeginAsync( inputData, cancellationToken );

            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                removedCount = await Repository.DeleteAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName ),
                    cancellationToken
                );
            }
            else if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                removedCount = await Repository.DeleteAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    cancellationToken
                );
            }

            if( removedCount > 0 )
            {
                await Repository.FlushAsync( cancellationToken );
            }

            var outputData = new DeleteOutputData(
                true,
                new DeleteOutputValue( removedCount ),
                null
            );

            await Presenter.HandleAsync( outputData, cancellationToken );
        }
    }
}

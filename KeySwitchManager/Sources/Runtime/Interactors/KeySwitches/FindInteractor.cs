using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Find;

using RkHelper.Primitives;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public sealed class FindInteractor : IFindUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IFindPresenter Presenter { get; }

        public FindInteractor(
            IKeySwitchRepository repository,
            IFindPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public async Task HandleAsync( FindInputData inputData, CancellationToken cancellationToken = default )
        {
            var developerName = inputData.Value.DeveloperName;
            var productName = inputData.Value.ProductName;
            var instrumentName = inputData.Value.InstrumentName;

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    new InstrumentName( instrumentName ),
                    cancellationToken
                );

                var output = new FindOutputData( new FindOutputValue( keySwitches ) );

                await Presenter.HandleAsync( output, cancellationToken );
                return;
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( developerName ),
                    new ProductName( productName ),
                    cancellationToken
                );

                var output = new FindOutputData( new FindOutputValue( keySwitches ) );

                await Presenter.HandleAsync( output, cancellationToken );
                return;
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( developerName ),
                    cancellationToken
                );

                var output = new FindOutputData( new FindOutputValue( keySwitches ) );

                await Presenter.HandleAsync( output, cancellationToken );
                return;
            }
            #endregion
        }
    }
}

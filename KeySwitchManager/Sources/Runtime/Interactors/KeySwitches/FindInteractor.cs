using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Find;

using RkHelper.Primitives;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class FindInteractor : IFindUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IFindPresenter Presenter { get; }

        public FindInteractor(
            IKeySwitchRepository repository ) :
            this( repository, new IFindPresenter.Null() )
        {}

        public FindInteractor(
            IKeySwitchRepository repository,
            IFindPresenter presenter )
        {
            Repository = repository;
            Presenter  = presenter;
        }

        public async Task<FindResponse> ExecuteAsync( FindRequest request, CancellationToken cancellationToken )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName ),
                    new InstrumentName( request.InstrumentName ),
                    cancellationToken
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName ),
                    cancellationToken
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                var keySwitches = await Repository.FindAsync(
                    new DeveloperName( request.DeveloperName ),
                    cancellationToken
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            return new FindResponse( new List<KeySwitch>() );
        }
    }
}

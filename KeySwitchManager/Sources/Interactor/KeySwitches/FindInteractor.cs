using System.Collections.Generic;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.UseCase.KeySwitches.Find;

using RkHelper.Text;

namespace KeySwitchManager.Interactor.KeySwitches
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

        public FindResponse Execute( FindRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            #region By Developer, Product, Instrument
            if( !StringHelper.IsEmpty( developerName, productName, instrumentName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName ),
                    new InstrumentName( request.InstrumentName )
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            #region By Developer, Product
            if( !StringHelper.IsEmpty( developerName, productName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName ),
                    new ProductName( request.ProductName )
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            #region By Developer
            if( !StringHelper.IsEmpty( developerName ) )
            {
                var keySwitches = Repository.Find(
                    new DeveloperName( request.DeveloperName )
                );

                return new FindResponse( keySwitches );
            }
            #endregion

            return new FindResponse( new List<KeySwitch>() );
        }
    }
}
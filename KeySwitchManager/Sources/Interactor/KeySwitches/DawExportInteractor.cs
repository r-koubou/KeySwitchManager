using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class DawExportInteractor : IDawExportUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IKeySwitchRepository OutputRepository { get; }
        private IDawExportPresenter Presenter { get; }

        public DawExportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository outputRepository ) :
            this( repository, outputRepository, new IDawExportPresenter.Null() )
        {}

        public DawExportInteractor(
            IKeySwitchRepository repository,
            IKeySwitchRepository outputRepository,
            IDawExportPresenter presenter )
        {
            Repository       = repository;
            OutputRepository = outputRepository;
            Presenter        = presenter;
        }

        public DawExportResponse Execute( DawExportRequest request )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var queryResult = SearchHelper.Search(
                Repository,
                request.Guid,
                developerName,
                productName,
                instrumentName
            );

            foreach( var x in queryResult )
            {
                Presenter.Present( $"Developer=\"{x.DeveloperName}\", Product=\"{x.ProductName}\", Instrument=\"{x.InstrumentName}\"" );
                OutputRepository.Save( x );
            }

            var flushed = OutputRepository.Flush();

            if( flushed == 0 )
            {
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new DawExportResponse( true, queryResult );
        }
    }
}
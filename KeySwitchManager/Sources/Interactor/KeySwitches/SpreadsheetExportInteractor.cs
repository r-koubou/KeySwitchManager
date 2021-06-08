using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet;

namespace KeySwitchManager.Interactor.KeySwitches
{
    public class SpreadsheetExportInteractor : ISpreadsheetExportUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private ISpreadsheetExportPresenter Presenter { get; }

        public SpreadsheetExportInteractor( IKeySwitchRepository outputRepository ) :
            this( outputRepository, new ISpreadsheetExportPresenter.Null() )
        {}

        public SpreadsheetExportInteractor(
            IKeySwitchRepository outputRepository,
            ISpreadsheetExportPresenter presenter )
        {
            Presenter        = presenter;
            OutputRepository = outputRepository;
        }

        public SpreadsheetExportResponse Execute( SpreadsheetExportRequest request )
        {
            foreach( var x in request.KeySwitches )
            {
                Presenter.Present( $"... {x.ProductName} | {x.InstrumentName}" );
                OutputRepository.Save( x );
            }

            var flushed = OutputRepository.Flush();

            if( flushed == 0 )
            {
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new SpreadsheetExportResponse(  true );
        }
    }
}
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ExportSpreadsheetInteractor : IExportSpreadsheetUseCase
    {
        private IKeySwitchRepository OutputRepository { get; }
        private IExportSpreadsheetPresenter Presenter { get; }

        public ExportSpreadsheetInteractor( IKeySwitchRepository outputRepository ) :
            this( outputRepository, new IExportSpreadsheetPresenter.Null() )
        {}

        public ExportSpreadsheetInteractor(
            IKeySwitchRepository outputRepository,
            IExportSpreadsheetPresenter presenter )
        {
            Presenter        = presenter;
            OutputRepository = outputRepository;
        }

        public ExportSpreadsheetResponse Execute( ExportSpreadsheetRequest request )
        {
            Presenter.Present( "Exporting. This process may take several minutes." );

            foreach( var x in request.KeySwitches )
            {
                OutputRepository.Save( x );
            }

            var flushed = OutputRepository.Flush();

            if( flushed == 0 )
            {
                Presenter.Present( $"No keyswitch(es) flushed to storage/repository ({OutputRepository.GetType()})" );
            }

            return new ExportSpreadsheetResponse( true );
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public class ExportFileInteractor : IExportFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IExportStrategy Strategy { get; }
        private IExportFilePresenter Presenter { get; }

        public ExportFileInteractor(
            IKeySwitchRepository repository,
            IExportStrategy strategy ) :
            this( repository, strategy, new IExportFilePresenter.Null() )
        {}

        public ExportFileInteractor(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IExportFilePresenter presenter )
        {
            Repository = repository;
            Strategy   = strategy;
            Presenter  = presenter;
        }

        async Task<ExportFileResponse> IExportFileUseCase.ExecuteAsync( ExportFileRequest request, IObserver<string>? loggingSubject )
        {
            var developerName = request.DeveloperName;
            var productName = request.ProductName;
            var instrumentName = request.InstrumentName;

            var queryResult = await SearchHelper.SearchAsync(
                Repository,
                developerName,
                productName,
                instrumentName
            );

            if( queryResult.Any() )
            {
                await Strategy.ExportAsync( queryResult );
                return new ExportFileResponse( queryResult );
            }

            Presenter.Present( $"No keyswitch(es) found. ({nameof( developerName )}={developerName}, {nameof( productName )}={productName}, {nameof( instrumentName )}={instrumentName})" );
            return new ExportFileResponse( new List<KeySwitch>() );
        }
    }
}

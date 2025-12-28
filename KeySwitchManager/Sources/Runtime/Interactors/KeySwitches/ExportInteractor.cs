using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Helpers;
using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.UseCase.KeySwitches.Export;

namespace KeySwitchManager.Interactors.KeySwitches
{
    public sealed class ExportInteractor : IExportFileUseCase
    {
        private IKeySwitchRepository Repository { get; }
        private IExportStrategy Strategy { get; }
        private IExportPresenter Presenter { get; }

        public ExportInteractor(
            IKeySwitchRepository repository,
            IExportStrategy strategy,
            IExportPresenter presenter )
        {
            Repository = repository;
            Strategy   = strategy;
            Presenter  = presenter;
        }

        public async Task HandleAsync( ExportInputData inputData, CancellationToken cancellationToken = default )
        {
            IDisposable? subscription = null;

            try
            {
                var developerName = inputData.Value.DeveloperName;
                var productName = inputData.Value.ProductName;
                var instrumentName = inputData.Value.InstrumentName;

                var queryResult = await SearchHelper.SearchAsync(
                    Repository,
                    developerName,
                    productName,
                    instrumentName,
                    cancellationToken
                );

                subscription = Strategy.OnExported.Subscribe( x =>
                    {
                        Presenter.HandleExportedAsync( x, cancellationToken );
                    }
                );

                await Strategy.ExportAsync( queryResult, cancellationToken );

                var outputData = new ExportOutputData( true, new ExportOutputValue( inputData.Value, queryResult ) );
                await Presenter.HandleAsync( outputData, cancellationToken );
            }
            catch( Exception e )
            {
                var outputData = new ExportOutputData( false, new ExportOutputValue( inputData.Value, new List<KeySwitch>() ), e );
                await Presenter.HandleAsync( outputData, cancellationToken );
            }
            finally
            {
                subscription?.Dispose();
            }
        }
    }
}

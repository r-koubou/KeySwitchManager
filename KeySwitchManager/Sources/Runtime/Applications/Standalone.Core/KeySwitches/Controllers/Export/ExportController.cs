using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Export
{
    public sealed class ExportController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IExportStrategy Strategy { get; }
        private DeveloperName DeveloperName { get; }
        private ProductName ProductName { get; }
        private InstrumentName InstrumentName { get; }
        private IExportPresenter Presenter { get; }

        public ExportController(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IKeySwitchRepository sourceRepository,
            IExportStrategy strategy,
            IExportPresenter presenter )
        {
            DeveloperName    = developerName;
            ProductName      = productName;
            InstrumentName   = instrumentName;
            SourceRepository = sourceRepository;
            Strategy           = strategy;
            Presenter        = presenter;
        }

        public void Dispose()
        {
            Disposer.Dispose( SourceRepository );
        }

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            IExportFileUseCase interactor = new ExportInteractor(
                SourceRepository,
                Strategy,
                Presenter
            );

            var inputValue = new ExportInputValue(
                DeveloperName.Value,
                ProductName.Value,
                InstrumentName.Value
            );

            await interactor.HandleAsync( new ExportInputData( inputValue ), cancellationToken );
        }
    }
}

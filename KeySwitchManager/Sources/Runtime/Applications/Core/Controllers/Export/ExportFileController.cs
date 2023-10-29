using System;
using System.Reactive.Subjects;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Export
{
    public class ExportFileController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IExportStrategy Strategy { get; }
        private DeveloperName DeveloperName { get; }
        private ProductName ProductName { get; }
        private InstrumentName InstrumentName { get; }
        private IExportFilePresenter Presenter { get; }
        private readonly Subject<string> logging = new();

        public ExportFileController(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IKeySwitchRepository sourceRepository,
            IExportStrategy strategy,
            IExportFilePresenter presenter )
        {
            DeveloperName    = developerName;
            ProductName      = productName;
            InstrumentName   = instrumentName;
            SourceRepository = sourceRepository;
            Strategy           = strategy;
            Presenter        = presenter;

            logging.Subscribe(
                onNext: presenter.Present,
                onError: presenter.Present
            );

        }

        public void Dispose()
        {
            Disposer.Dispose( logging );
            Disposer.Dispose( SourceRepository );
        }

        async Task IController.ExecuteAsync()
        {
            IExportFileUseCase interactor = new ExportFileInteractor(
                SourceRepository,
                Strategy,
                Presenter
            );

            var response = await interactor.ExecuteAsync(
                new ExportFileRequest(
                    DeveloperName.Value,
                    ProductName.Value,
                    InstrumentName.Value
                ),
                logging
            );

            Presenter.Complete( response );
        }
    }
}

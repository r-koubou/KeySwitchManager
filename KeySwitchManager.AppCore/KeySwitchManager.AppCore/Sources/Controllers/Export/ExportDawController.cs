using System;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Domain.KeySwitches.Models.Values;
using KeySwitchManager.Interactor.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Export.Daw;

namespace KeySwitchManager.AppCore.Controllers.Export
{
    public class ExportDawController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IKeySwitchRepository TargetRepository { get; }
        private DeveloperName DeveloperName { get; }
        private ProductName ProductName { get; }
        private InstrumentName InstrumentName { get; }
        private IExportDawPresenter Presenter { get; }

        public ExportDawController(
            DeveloperName developerName,
            ProductName productName,
            InstrumentName instrumentName,
            IKeySwitchRepository sourceRepository,
            IKeySwitchRepository targetRepository,
            IExportDawPresenter presenter )
        {
            SourceRepository = sourceRepository;
            Presenter        = presenter;
            InstrumentName   = instrumentName;
            DeveloperName    = developerName;
            ProductName      = productName;
            TargetRepository = targetRepository;
        }

        public void Dispose()
        {
            DisposeSafety( TargetRepository );
            DisposeSafety( SourceRepository );
        }

        private static void DisposeSafety( IDisposable disposable )
        {
            try
            {
                disposable.Dispose();
            }
            catch
            {
                // ignored
            }
        }

        public void Execute()
        {
            var interactor = new ExportDawInteractor(
                SourceRepository,
                TargetRepository,
                Presenter
            );

            var response = interactor.Execute(
                new ExportDawRequest(
                    DeveloperName.Value,
                    ProductName.Value,
                    InstrumentName.Value
                )
            );

            Presenter.Complete( response );
        }
    }
}

using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.Commons;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Dump
{
    public sealed class DumpController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IExportStrategy Strategy { get; }
        private IDumpPresenter Presenter { get; }

        public DumpController(
            IKeySwitchRepository sourceRepository,
            IExportStrategy strategy,
            IDumpPresenter presenter )
        {
            SourceRepository = sourceRepository;
            Strategy         = strategy;
            Presenter        = presenter;
        }

        public void Dispose()
        {
            Disposer.Dispose( SourceRepository );
        }

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            IDumpUseCase interactor = new DumpInteractor(
                SourceRepository,
                Strategy,
                Presenter
            );

            await interactor.HandleAsync( UnitInputData.Default, cancellationToken );
        }
    }
}

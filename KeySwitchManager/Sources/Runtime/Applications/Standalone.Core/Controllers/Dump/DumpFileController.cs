using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Dump;
using KeySwitchManager.UseCase.KeySwitches.Export;

using RkHelper.System;

namespace KeySwitchManager.Applications.Standalone.Core.Controllers.Dump
{
    public class DumpFileController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IExportStrategy Strategy { get; }
        private IDumpFilePresenter Presenter { get; }

        public DumpFileController(
            IKeySwitchRepository sourceRepository,
            IExportStrategy strategy,
            IDumpFilePresenter presenter )
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
            IDumpFileUseCase interactor = new DumpFileInteractor(
                SourceRepository,
                Strategy,
                Presenter
            );

            var response = await interactor.ExecuteAsync( new DumpFileRequest(), cancellationToken );
            Presenter.Complete( response );
        }
    }
}

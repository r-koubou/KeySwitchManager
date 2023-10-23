using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Dump;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Dump
{
    public class DumpFileController : IController
    {
        private IKeySwitchRepository SourceRepository { get; }
        private IKeySwitchWriter Writer { get; }
        private IDumpFilePresenter Presenter { get; }

        public DumpFileController(
            IKeySwitchRepository sourceRepository,
            IKeySwitchWriter writer,
            IDumpFilePresenter presenter )
        {
            SourceRepository = sourceRepository;
            Writer           = writer;
            Presenter        = presenter;
        }

        public void Dispose()
        {
            Disposer.Dispose( SourceRepository );

            if( !Writer.LeaveOpen )
            {
                Disposer.Dispose( Writer );
            }
        }

        async Task IController.ExecuteAsync()
        {
            IDumpFileUseCase interactor = new DumpFileInteractor(
                SourceRepository,
                Writer,
                Presenter
            );

            var response = await interactor.ExecuteAsync( new DumpFileRequest() );
            Presenter.Complete( response );
        }
    }
}

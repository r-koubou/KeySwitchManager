using System;
using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public class ImportFileController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private IImportContentReader ContentContentReader { get; }
        private IContent Content { get; }
        private IImportFilePresenter Presenter { get; }
        private bool LeaveOpen { get; }

        private readonly IDisposable loggingSubscriber;

        #region Ctor
        public ImportFileController(
            IKeySwitchRepository databaseRepository,
            IImportContentReader contentReader,
            IContent content,
            IImportFilePresenter presenter,
            bool leaveOpen = false )
        {
            DatabaseRepository   = databaseRepository;
            ContentContentReader = contentReader;
            Content              = content;
            Presenter            = presenter;
            LeaveOpen            = leaveOpen;
            loggingSubscriber    = databaseRepository.OnLogging.Subscribe( onNext: presenter.Present, onError: presenter.Present );
        }
        #endregion

        public void Dispose()
        {
            Disposer.Dispose( loggingSubscriber );

            if( LeaveOpen )
            {
                return;
            }

            Disposer.Dispose( DatabaseRepository );
        }

        async Task IController.ExecuteAsync( CancellationToken cancellationToken )
        {
            IImportFileUseCase interactor = new ImportFileInteractor( DatabaseRepository, Presenter );
            var request = new ImportFileRequest( ContentContentReader, Content );
            var response = await interactor.ExecuteAsync( request, cancellationToken );
            await DatabaseRepository.FlushAsync( cancellationToken );
            Presenter.Complete( response );
        }

    }
}

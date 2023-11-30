using System.Threading;
using System.Threading.Tasks;

using KeySwitchManager.Controllers.KeySwitches;
using KeySwitchManager.Domain.KeySwitches;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;

using RkHelper.System;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Controllers.Import
{
    public sealed class ImportController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private IImportContentReader ContentContentReader { get; }
        private IContent Content { get; }
        private IImportFilePresenter Presenter { get; }
        private bool LeaveOpen { get; }

        #region Ctor
        public ImportController(
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
        }
        #endregion

        public void Dispose()
        {
            if( LeaveOpen )
            {
                return;
            }

            Disposer.Dispose( DatabaseRepository );
        }

        public async Task ExecuteAsync( CancellationToken cancellationToken )
        {
            IImportUseCase interactor = new ImportInteractor( DatabaseRepository, Presenter );
            var inputValue = new ImportInputValue( ContentContentReader, Content );
            var inputData = new ImportInputData( inputValue );

            await interactor.HandleAsync( inputData, cancellationToken );
        }
    }
}

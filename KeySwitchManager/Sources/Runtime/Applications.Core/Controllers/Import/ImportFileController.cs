using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Import;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Import
{
    public class ImportFileController : IController
    {
        private IKeySwitchRepository DatabaseRepository { get; }
        private IKeySwitchReader KeySwitchReader { get; }
        private IImportFilePresenter Presenter { get; }
        private bool LeaveOpen { get; }

        #region Ctor
        public ImportFileController(
            IKeySwitchRepository databaseRepository,
            IKeySwitchReader reader,
            IImportFilePresenter presenter,
            bool leaveOpen = false )
        {
            DatabaseRepository = databaseRepository;
            KeySwitchReader    = reader;
            Presenter          = presenter;
            LeaveOpen          = leaveOpen;
        }
        #endregion

        public void Dispose()
        {
            if( LeaveOpen )
            {
                return;
            }

            Disposer.Dispose( DatabaseRepository );

            if( !KeySwitchReader.LeaveOpen )
            {
                Disposer.Dispose( KeySwitchReader );
            }
        }

        public void Execute()
        {
            var keySwitches = KeySwitchReader.Read();
            var interactor = new ImportFileInteractor( DatabaseRepository, Presenter );
            var request = new ImportFileRequest( keySwitches );
            var response = interactor.Execute( request );
            Presenter.Complete( response );
        }

    }
}

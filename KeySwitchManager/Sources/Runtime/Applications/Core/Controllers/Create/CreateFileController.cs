using KeySwitchManager.Domain.KeySwitches.Models;
using KeySwitchManager.Interactors.KeySwitches;
using KeySwitchManager.UseCase.KeySwitches.Create;

using RkHelper.System;

namespace KeySwitchManager.Applications.Core.Controllers.Create
{
    public class CreateFileController : IController
    {
        private IKeySwitchWriter Writer { get; }
        private ICreateFilePresenter Presenter { get; }

        public CreateFileController(
            IKeySwitchWriter writer,
            ICreateFilePresenter presenter )
        {
            Writer    = writer;
            Presenter = presenter;
        }

        public void Dispose()
        {
            if( !Writer.LeaveOpen )
            {
                Disposer.Dispose( Writer );
            }
        }

        public void Execute()
        {
            var interactor = new CreateFileInteractor( Presenter );
            var response = interactor.Execute( new CreateFileRequest( Writer ) );
            Presenter.Complete( response );
        }
    }
}

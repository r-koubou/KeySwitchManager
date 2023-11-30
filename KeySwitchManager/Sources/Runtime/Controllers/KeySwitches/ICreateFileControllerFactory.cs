using KeySwitchManager.UseCase.KeySwitches.Create;

namespace KeySwitchManager.Controllers.KeySwitches
{
    public interface ICreateFileControllerFactory
    {
        IController Create( string outputFilePath, ICreatePresenter presenter );
    }
}

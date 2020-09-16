using KeySwitchManager.UseCases.KeySwitches.Removing;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IRemovingPresenter : IPresenter<RemovingResponse>
    {
        public class Null : IRemovingPresenter
        {}
    }
}
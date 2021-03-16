using KeySwitchManager.UseCases.KeySwitch.Adding;

namespace KeySwitchManager.Presenters.KeySwitch
{
    public interface IAddingPresenter : IPresenter<KeySwitchAddingResponse>
    {
        public class Null : IAddingPresenter
        {
            public void Complete( KeySwitchAddingResponse response )
            {}
        }
    }
}
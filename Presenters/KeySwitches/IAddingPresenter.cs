using KeySwitchManager.UseCases.KeySwitches.Adding;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IAddingPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IAddingPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}
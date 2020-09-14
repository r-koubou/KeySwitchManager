using ArticulationManager.UseCases.KeySwitches.Adding;

namespace ArticulationManager.Presenters.KeySwitches
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
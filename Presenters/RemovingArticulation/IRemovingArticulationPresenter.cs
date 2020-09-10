using ArticulationManager.UseCases.RemovingArticulation;

namespace ArticulationManager.Presenters.RemovingArticulation
{
    public interface IRemovingArticulationPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IRemovingArticulationPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}
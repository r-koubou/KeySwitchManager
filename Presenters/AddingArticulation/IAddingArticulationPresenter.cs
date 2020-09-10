using ArticulationManager.UseCases.AddingArticulation;

namespace ArticulationManager.Presenters.AddingArticulation
{
    public interface IAddingArticulationPresenter : IPresenter<OutputData>
    {
        public class Null : IAddingArticulationPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}
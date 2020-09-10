using ArticulationManager.UseCases.AddingArticulation;

namespace ArticulationManager.Presenters.AddingArticulation
{
    public interface IAddingArticulationPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IAddingArticulationPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}
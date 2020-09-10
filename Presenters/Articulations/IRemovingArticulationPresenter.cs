using ArticulationManager.UseCases.Articulations.RemovingFromDatabase;

namespace ArticulationManager.Presenters.Articulations
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
using ArticulationManager.UseCases.Articulations.Database.Adding;

namespace ArticulationManager.Presenters.Articulations.Database
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
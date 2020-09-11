using ArticulationManager.UseCases.Articulations.Exporting.Text;

namespace ArticulationManager.Presenters.Articulations
{
    public interface IExportingTextPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IExportingTextPresenter
        {
            public void Output( OutputData outputData )
            {}
        }
    }
}
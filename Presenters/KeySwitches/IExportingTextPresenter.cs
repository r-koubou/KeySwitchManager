using ArticulationManager.UseCases.KeySwitches.Exporting.Text;

namespace ArticulationManager.Presenters.KeySwitches
{
    public interface IExportingTextPresenter
    {
        public void Output( OutputData outputData );

        public class Null : IExportingTextPresenter
        {
            public void Output( OutputData outputData )
            {}
        }

        public class Console : IExportingTextPresenter
        {
            public void Output( OutputData outputData )
            {
                System.Console.WriteLine( outputData.Text.Value );
            }
        }

    }
}
using KeySwitchManager.UseCases.KeySwitches.Exporting.Text;

namespace KeySwitchManager.Presenters.KeySwitches
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
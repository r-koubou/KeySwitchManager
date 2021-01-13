using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IExportingTextPresenter : IPresenter<ExportingTextResponse>
    {
        public class Null : IExportingTextPresenter
        {
            public void Complete( ExportingTextResponse response )
            {}
        }

        public class Console : IExportingTextPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
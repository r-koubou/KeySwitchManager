using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IExportingXlsxPresenter: IPresenter<ExportingXlsxResponse>
    {
        public class Null : IExportingXlsxPresenter
        {
            public void Complete( ExportingXlsxResponse response )
            {}
        }

        public class Console : IExportingXlsxPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
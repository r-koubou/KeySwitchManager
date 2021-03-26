using KeySwitchManager.UseCases.KeySwitches.Exporting;

namespace KeySwitchManager.Presenters.KeySwitch
{
    public interface IExportingTemplateXlsxPresenter: IPresenter<ExportingTemplateXlsxResponse>
    {
        public class Null : IExportingTemplateXlsxPresenter
        {
            public void Complete( ExportingTemplateXlsxResponse response )
            {}
        }

        public class Console : IExportingTemplateXlsxPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
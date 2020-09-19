using KeySwitchManager.UseCases.VstExpressionMap.Exporting;

namespace KeySwitchManager.Presenters.VstExpressionMap
{
    public interface IExportingVstExpressionMapPresenter: IPresenter<ExportingVstExpressionMapResponse>
    {
        public class Null : IExportingVstExpressionMapPresenter
        {
            public void Complete( ExportingVstExpressionMapResponse response )
            {}
        }

        public class Console : IExportingVstExpressionMapPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
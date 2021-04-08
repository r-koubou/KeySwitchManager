using KeySwitchManager.Presenters.Commons;
using KeySwitchManager.UseCases.KeySwitches.VstExpressionMap.Exporting;

namespace KeySwitchManager.Presenters.KeySwitches.VstExpressionMap
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
using KeySwitchManager.Presenters.Commons;
using KeySwitchManager.UseCases.KeySwitches.StudioOne.Exporting;

namespace KeySwitchManager.Presenters.KeySwitches.StudioOneKeySwitch
{
    public interface IExportingStudioOneKeySwitchPresenter: IPresenter<ExportingStudioOneKeySwitchResponse>
    {
        public class Null : IExportingStudioOneKeySwitchPresenter
        {
            public void Complete( ExportingStudioOneKeySwitchResponse response )
            {}
        }

        public class Console : IExportingStudioOneKeySwitchPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
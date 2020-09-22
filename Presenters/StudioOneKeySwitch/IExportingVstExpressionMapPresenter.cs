using KeySwitchManager.UseCases.StudioOneKeySwitch.Exporting;

namespace KeySwitchManager.Presenters.StudioOneKeySwitch
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
using KeySwitchManager.Presenters.Commons;
using KeySwitchManager.UseCases.KeySwitches.Cakewalk.Exporting;

namespace KeySwitchManager.Presenters.KeySwitch.Cakewalk
{
    public interface IExportingCakewalkArticulationPresenter: IPresenter<ExportingCakewalkArticulationResponse>
    {
        public class Null : IExportingCakewalkArticulationPresenter
        {
            public void Complete( ExportingCakewalkArticulationResponse response )
            {}
        }

        public class Console : IExportingCakewalkArticulationPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
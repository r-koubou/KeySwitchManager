using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public interface IExportDawPresenter: IPresenter<ExportDawResponse>
    {
        public class Null : IExportDawPresenter
        {
            public void Complete( ExportDawResponse response )
            {}
        }

        public class Console : IExportDawPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }

            public void Message( string message )
            {
                System.Console.WriteLine( message );
            }
        }
    }
}
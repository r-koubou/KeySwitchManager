using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Daw
{
    public interface IDawExportPresenter: IPresenter<DawExportResponse>
    {
        public class Null : IDawExportPresenter
        {
            public void Complete( DawExportResponse response )
            {}
        }

        public class Console : IDawExportPresenter
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
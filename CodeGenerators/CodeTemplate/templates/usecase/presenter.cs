using KeySwitchManager.UseCase.Commons;

namespace ${namespace}
{
    public interface I${name}Presenter : IPresenter<${name}Response>
    {
        public class Null : I${name}Presenter
        {
            public void Complete( ${name}Response response )
            {}
        }

        public class Console : I${name}Presenter
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
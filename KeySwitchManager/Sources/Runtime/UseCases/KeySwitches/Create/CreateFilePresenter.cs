using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreatePresenter : IPresenter<CreateResponse>
    {
        public class Null : ICreatePresenter
        {
            public void Complete( CreateResponse response )
            {}
        }

        public class Console : ICreatePresenter
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

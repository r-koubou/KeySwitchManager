using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create
{
    public interface ICreateFilePresenter : IPresenter<CreateFileResponse>
    {
        public class Null : ICreateFilePresenter
        {
            public void Complete( CreateFileResponse response )
            {}
        }

        public class Console : ICreateFilePresenter
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
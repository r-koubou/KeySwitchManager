using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Delete
{
    public interface IDeletePresenter : IPresenter<DeleteResponse>
    {
        public class Null : IDeletePresenter
        {}

        public class Console : IDeletePresenter
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
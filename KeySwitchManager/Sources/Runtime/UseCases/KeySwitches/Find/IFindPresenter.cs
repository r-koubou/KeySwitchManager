using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Find
{
    public interface IFindPresenter : IPresenter<FindResponse>
    {
        public class Null : IFindPresenter
        {}

        public class Console : IFindPresenter
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
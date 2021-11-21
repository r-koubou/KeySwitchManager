using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Dump
{
    public interface IDumpFilePresenter : IPresenter<DumpFileResponse>
    {
        public class Null : IDumpFilePresenter
        {
            public void Complete( DumpFileResponse response )
            {}
        }

        public class Console : IDumpFilePresenter
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
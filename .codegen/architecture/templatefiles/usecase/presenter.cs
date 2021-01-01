using System.IO;

namespace path.to.your.ns
{
    public interface I__name__Presenter : IPresenter<__name__Response>
    {
        public class Null : I__name__Presenter
        {
            public void Dispose()
            {}
        }

        public class Console : I__name__Presenter
        {
            public void Dispose()
            {}

            #region IPresenter
            public void Start()
            {}

            public void Progress( float progress )
            {}

            public void Complete( __name__Response response )
            {}

            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
            #endregion
        }

        public class LogFile : I__name__Presenter
        {
            private StreamWriter Writer {get;}

            public LogFile( string path )
            {
                Writer = File.CreateText( path );
            }

            public void Dispose()
            {
                Writer.Dispose();
            }

            #region IPresenter
            public void Start()
            {}

            public void Progress( float progress )
            {}

            public void Complete( __name__Response response )
            {}

            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
            #endregion
        }
    }
}
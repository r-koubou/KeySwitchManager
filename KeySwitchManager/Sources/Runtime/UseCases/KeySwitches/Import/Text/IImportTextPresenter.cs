using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public interface IImportTextPresenter : IPresenter<ImportTextResponse>
    {
        public class Null : IImportTextPresenter
        {
            public void Complete( ImportTextResponse response )
            {}
        }

        public class Console : IImportTextPresenter
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
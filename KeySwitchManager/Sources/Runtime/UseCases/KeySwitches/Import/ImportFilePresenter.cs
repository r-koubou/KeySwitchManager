using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public interface IImportFilePresenter : IPresenter<ImportFileResponse>
    {
        public class Null : IImportFilePresenter
        {
            public void Complete( ImportFileResponse response )
            {}
        }

        public class Console : IImportFilePresenter
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
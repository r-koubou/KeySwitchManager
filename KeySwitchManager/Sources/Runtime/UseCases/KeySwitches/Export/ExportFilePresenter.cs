using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export
{
    public interface IExportFilePresenter : IPresenter<ExportFileResponse>
    {
        public class Null : IExportFilePresenter
        {
            public void Complete( ExportFileResponse response )
            {}
        }

        public class Console : IExportFilePresenter
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
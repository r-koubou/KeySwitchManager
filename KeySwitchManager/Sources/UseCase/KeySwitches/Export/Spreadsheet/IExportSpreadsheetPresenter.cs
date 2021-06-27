using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public interface IExportSpreadsheetPresenter: IPresenter<ExportSpreadsheetResponse>
    {
        public class Null : IExportSpreadsheetPresenter
        {
            public void Complete( ExportSpreadsheetResponse response )
            {}
        }

        public class Console : IExportSpreadsheetPresenter
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
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Export.Spreadsheet
{
    public interface ISpreadsheetExportPresenter: IPresenter<SpreadsheetExportResponse>
    {
        public class Null : ISpreadsheetExportPresenter
        {
            public void Complete( SpreadsheetExportResponse response )
            {}
        }

        public class Console : ISpreadsheetExportPresenter
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
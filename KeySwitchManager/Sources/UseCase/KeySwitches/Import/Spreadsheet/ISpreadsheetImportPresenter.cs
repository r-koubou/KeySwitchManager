using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public interface ISpreadsheetImportPresenter : IPresenter<SpreadsheetImportResponse>
    {
        public class Null : ISpreadsheetImportPresenter
        {}

        public class Console : ISpreadsheetImportPresenter
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
using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Spreadsheet
{
    public interface IImportSpreadsheetPresenter : IPresenter<ImportSpreadSheetResponse>
    {
        public class Null : IImportSpreadsheetPresenter
        {}

        public class Console : IImportSpreadsheetPresenter
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
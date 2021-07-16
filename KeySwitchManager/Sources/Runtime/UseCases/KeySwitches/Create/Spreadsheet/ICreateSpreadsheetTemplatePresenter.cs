using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create.Spreadsheet
{
    public interface ICreateSpreadsheetTemplatePresenter: IPresenter<CreateSpreadsheetTemplateResponse>
    {
        public class Null : ICreateSpreadsheetTemplatePresenter
        {
            public void Complete( CreateSpreadsheetTemplateResponse response )
            {}
        }

        public class Console : ICreateSpreadsheetTemplatePresenter
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
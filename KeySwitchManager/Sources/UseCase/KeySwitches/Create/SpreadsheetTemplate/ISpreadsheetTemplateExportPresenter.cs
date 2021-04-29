using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create.SpreadsheetTemplate
{
    public interface ISpreadsheetTemplateExportPresenter: IPresenter<SpreadsheetTemplateExportResponse>
    {
        public class Null : ISpreadsheetTemplateExportPresenter
        {
            public void Complete( SpreadsheetTemplateExportResponse response )
            {}
        }

        public class Console : ISpreadsheetTemplateExportPresenter
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
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IImportingXlsxPresenter : IPresenter<ImportingTextResponse>
    {
        public class Null : IImportingXlsxPresenter
        {}

        public class Console : IImportingXlsxPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
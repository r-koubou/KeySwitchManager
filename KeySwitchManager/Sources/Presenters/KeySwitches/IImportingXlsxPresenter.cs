using KeySwitchManager.UseCases.KeySwitch.Importing.Xlsx;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IImportingXlsxPresenter : IPresenter<ImportingXlsxRequest>
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
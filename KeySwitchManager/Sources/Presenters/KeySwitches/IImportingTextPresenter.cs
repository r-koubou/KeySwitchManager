using KeySwitchManager.Presenters.Commons;
using KeySwitchManager.UseCases.KeySwitches.Importing.Text;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface IImportingTextPresenter : IPresenter<ImportingTextResponse>
    {
        public class Null : IImportingTextPresenter
        {}

        public class Console : IImportingTextPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
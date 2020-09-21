using KeySwitchManager.UseCases.KeySwitches.Removing;
using KeySwitchManager.UseCases.KeySwitches.Searching;

namespace KeySwitchManager.Presenters.KeySwitches
{
    public interface ISearchingPresenter : IPresenter<SearchingResponse>
    {
        public class Null : ISearchingPresenter
        {}

        public class Console : ISearchingPresenter
        {
            public void Present<T>( T param )
            {
                System.Console.WriteLine( param );
            }
        }
    }
}
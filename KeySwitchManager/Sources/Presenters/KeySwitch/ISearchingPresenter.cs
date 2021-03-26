using KeySwitchManager.UseCases.KeySwitches.Searching;

namespace KeySwitchManager.Presenters.KeySwitch
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
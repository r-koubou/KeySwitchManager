using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create.Text
{
    public interface ICreateTextTemplatePresenter : IPresenter<CreateTextTemplateResponse>
    {
        public class Null : ICreateTextTemplatePresenter
        {
            public void Complete( CreateTextTemplateResponse response )
            {}
        }

        public class Console : ICreateTextTemplatePresenter
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
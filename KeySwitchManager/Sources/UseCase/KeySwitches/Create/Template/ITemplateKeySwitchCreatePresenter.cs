using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Create.Template
{
    public interface ITemplateKeySwitchCreatePresenter : IPresenter<TemplateKeySwitchCreateResponse>
    {
        public class Null : ITemplateKeySwitchCreatePresenter
        {
            public void Complete( TemplateKeySwitchCreateResponse response )
            {}
        }

        public class Console : ITemplateKeySwitchCreatePresenter
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
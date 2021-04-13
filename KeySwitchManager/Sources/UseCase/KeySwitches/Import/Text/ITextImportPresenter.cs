using KeySwitchManager.UseCase.Commons;

namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public interface ITextImportPresenter : IPresenter<TextImportResponse>
    {
        public class Null : ITextImportPresenter
        {
            public void Complete( TextImportResponse response )
            {}
        }

        public class Console : ITextImportPresenter
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
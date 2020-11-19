using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitches.Importing.Text
{
    public class ImportingTextRequest
    {
        public IText JsonText { get; }

        public ImportingTextRequest( IText jsonText )
        {
            JsonText = jsonText;
        }
        public ImportingTextRequest( string jsonText )
        {
            JsonText = new PlainText( jsonText );
        }
    }
}
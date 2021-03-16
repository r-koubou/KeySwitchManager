using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public class ExportingTextResponse
    {
        public int Count { get; }

        public IText Text { get; }

        public ExportingTextResponse()
            : this( new PlainText( "" ), 0 )
        {}

        public ExportingTextResponse( IText text, int count )
        {
            Text  = text;
            Count = count;
        }
    }
}
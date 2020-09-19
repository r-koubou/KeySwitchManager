using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitches.Exporting.Text
{
    public class ExportingTextResponse
    {
        public bool Found { get; set; }

        public IText Text { get; }

        public ExportingTextResponse( IText text )
        {
            Text = text;
        }
    }
}
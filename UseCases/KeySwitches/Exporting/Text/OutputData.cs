using KeySwitchManager.Domain.Commons;

namespace KeySwitchManager.UseCases.KeySwitches.Exporting.Text
{
    public class OutputData
    {
        public IText Text { get; }

        public OutputData( IText text )
        {
            Text = text;
        }
    }
}
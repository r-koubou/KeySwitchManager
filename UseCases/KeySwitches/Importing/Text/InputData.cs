using ArticulationManager.Domain.Commons;

namespace ArticulationManager.UseCases.KeySwitches.Importing.Text
{
    public class InputData
    {
        public IText JsonText { get; }

        public InputData( IText jsonText )
        {
            JsonText = jsonText;
        }
        public InputData( string jsonText )
        {
            JsonText = new PlainText( jsonText );
        }
    }
}
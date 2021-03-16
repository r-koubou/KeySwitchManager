namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public class ExportingTemplateAsTextResponse
    {
        public string Text { get; }

        public ExportingTemplateAsTextResponse( string text )
        {
            Text = text;
        }
    }
}
namespace KeySwitchManager.UseCases.KeySwitches.Exporting
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
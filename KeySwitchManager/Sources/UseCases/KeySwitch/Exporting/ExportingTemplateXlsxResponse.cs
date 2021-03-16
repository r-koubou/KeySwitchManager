namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public class ExportingTemplateXlsxResponse
    {
        public bool Result { get; }

        public ExportingTemplateXlsxResponse( bool result )
        {
            Result = result;
        }
    }
}
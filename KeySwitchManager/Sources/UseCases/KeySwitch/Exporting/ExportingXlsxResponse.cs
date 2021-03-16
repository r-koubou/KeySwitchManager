namespace KeySwitchManager.UseCases.KeySwitch.Exporting
{
    public class ExportingXlsxResponse
    {
        public bool Result { get; }

        public ExportingXlsxResponse( bool result )
        {
            Result = result;
        }
    }
}
namespace KeySwitchManager.UseCases.KeySwitch.Importing.Text
{
    public interface IImportingTextUseCase
    {
        public ImportingTextResponse Execute( ImportingTextRequest request );
    }
}
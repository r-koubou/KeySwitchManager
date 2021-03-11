namespace KeySwitchManager.UseCases.KeySwitches.Importing.Text
{
    public interface IImportingTextUseCase
    {
        public ImportingTextResponse Execute( ImportingTextRequest request );
    }
}
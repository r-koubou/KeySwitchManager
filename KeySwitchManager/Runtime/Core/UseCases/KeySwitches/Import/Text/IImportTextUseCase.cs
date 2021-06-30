namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public interface IImportTextUseCase
    {
        public ImportTextResponse Execute( ImportTextRequest request );
    }
}
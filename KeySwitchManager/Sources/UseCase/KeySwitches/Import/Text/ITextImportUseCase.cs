namespace KeySwitchManager.UseCase.KeySwitches.Import.Text
{
    public interface ITextImportUseCase
    {
        public TextImportResponse Execute( TextImportRequest importRequest );
    }
}
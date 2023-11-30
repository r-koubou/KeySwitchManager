namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public sealed class ImportInputValue
    {
        public IImportContentReader ContentReader { get; }
        public IContent Content { get; }

        public ImportInputValue( IImportContentReader contentReader, IContent content )
        {
            ContentReader = contentReader;
            Content       = content;
        }
    }
}

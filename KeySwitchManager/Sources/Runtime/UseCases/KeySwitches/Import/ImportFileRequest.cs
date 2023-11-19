namespace KeySwitchManager.UseCase.KeySwitches.Import
{
    public class ImportFileRequest
    {
        public IImportContentReader ContentReader { get; }
        public IContent Content { get; }

        public ImportFileRequest( IImportContentReader contentReader, IContent content )
        {
            ContentReader = contentReader;
            Content       = content;
        }
    }
}

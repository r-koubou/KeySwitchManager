using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructures.Storage.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Spreadsheet.ClosedXml.KeySwitches.Import;
using KeySwitchManager.Infrastructures.Storage.Yaml.KeySwitches.Import;
using KeySwitchManager.UseCase.KeySwitches.Import;

namespace KeySwitchManager.Applications.Standalone.Core.KeySwitches.Helpers
{
    public static class ImportContentFactory
    {
        public static (IContent content, IImportContentReader contentReader) CreateFromLocalFile( string importFilePath )
        {
            IImportContentReader contentReader;
            var fileExtension = Path.GetExtension( importFilePath ).ToLower();

            switch( Path.GetExtension( importFilePath ).ToLower() )
            {
                case ".yaml":
                case ".yml":
                    contentReader = new YamlImportContentReader();
                    break;
                case ".xlsx":
                    contentReader = new ClosedXmlImportContentReader();
                    break;
                default:
                    throw new InvalidDataException( $"Unknown file extension: {fileExtension}" );
            }

            IContent content = new FileContent( new FilePath( importFilePath ) );

            return ( content, contentReader );
        }
    }
}

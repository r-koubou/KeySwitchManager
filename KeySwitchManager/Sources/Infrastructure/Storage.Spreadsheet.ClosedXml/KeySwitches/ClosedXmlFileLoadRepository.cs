using System.IO;

using KeySwitchManager.Commons.Data;
using KeySwitchManager.Infrastructure.Storage.KeySwitches;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches.Helper;
using KeySwitchManager.Infrastructure.Storage.Spreadsheet.KeySwitches.Translators;

using RkHelper.IO;

namespace KeySwitchManager.Infrastructure.Storage.Spreadsheet.ClosedXml.KeySwitches
{
    public class ClosedXmlFileLoadRepository : LoadOnlyKeySwitchFileRepository
    {
        private const int InitialBufferSize = 1024 * 64;

        public ClosedXmlFileLoadRepository( FilePath path, bool loadFromPathNow = true ) :
            base( path, false )
        // A param loadFromPathNow is always false by Since we have implemented this class own processing
        {
            if( loadFromPathNow )
            {
                Load();
            }
        }

        #region Load from file
        public sealed override void Load()
        {
            if( !DataPath.Exists )
            {
                throw new FileNotFoundException( DataPath.Path );
            }

            using var stream = new FileStream( DataPath.Path, FileMode.Open );
            using var memory = new MemoryStream( InitialBufferSize );

            StreamHelper.ReadAllAndWrite( stream, memory );
            byte[] xlsxBytes = memory.ToArray();

            var workBook = XlsxWorkBookParsingHelper.Parse( xlsxBytes );
            var translator = new SpreadsheetImportTranslator();

            KeySwitches.Clear();
            KeySwitches.AddRange( translator.Translate( workBook ) );

        }
        #endregion
    }
}
